namespace PSO_optimisation_of_LJ_clusters;
using System.Globalization;

 class PSO
    {
        private readonly Particle[] particles;
        private double[] gbestPos;
        private double gbestValue = double.MaxValue;
        private readonly int swarmSize;
        private readonly int N;

        public PSO(int swarmSize, int N)
        {
            this.swarmSize = swarmSize;
            this.N = N;

            Random rng = new Random();

            gbestPos = new double[N * 3];
            particles = new Particle[swarmSize];

            for (int i = 0; i < swarmSize; i++)
                particles[i] = new Particle(N, rng);
        }

        private void UpdateGBest()
        {
            foreach (Particle p in particles)
            {
                if (p.pbestValue < gbestValue)
                {
                    gbestValue = p.pbestValue;
                    Array.Copy(p.pbestPos, gbestPos, N * 3);
                }
            }
        }

        private void UpdateLBest(double[][] lbest, double[] lbestValue)
        {
            for (int i = 0; i < swarmSize; i++)
            {
                int left = (i - 1 + swarmSize) % swarmSize;
                int right = (i + 1) % swarmSize;

                lbestValue[i] = particles[i].pbestValue;
                Array.Copy(particles[i].pbestPos, lbest[i], N * 3);

                if (particles[left].pbestValue < lbestValue[i])
                {
                    lbestValue[i] = particles[left].pbestValue;
                    Array.Copy(particles[left].pbestPos, lbest[i], N * 3);
                }

                if (particles[right].pbestValue < lbestValue[i])
                {
                    lbestValue[i] = particles[right].pbestValue;
                    Array.Copy(particles[right].pbestPos, lbest[i], N * 3);
                }
            }
        }

        public void RunStarTopology()
        {
            Console.WriteLine("=== PSO — Star topology ===");

            while (LJCluster.numEvaluations < LJCluster.MAX_EVALUATIONS)
            {
                foreach (Particle p in particles)
                {
                    double energy = LJCluster.CalculateEnergy(p.positions, N);
                    if (energy == double.MaxValue) break;
                    p.UpdatePBest(energy);
                }

                UpdateGBest();

                foreach (Particle p in particles)
                {
                    p.CalculateVelocity(gbestPos);
                    p.UpdatePosition();
                }
            }

            PrintResults();
        }

        public void RunRingTopology()
        {
            Console.WriteLine("=== PSO — Ring topology ===");

            double[][] lbest      = new double[swarmSize][];
            double[]   lbestValue = new double[swarmSize];

            for (int i = 0; i < swarmSize; i++)
                lbest[i] = new double[N * 3];

            UpdateLBest(lbest, lbestValue);

            while (LJCluster.numEvaluations < LJCluster.MAX_EVALUATIONS)
            {
                foreach (Particle p in particles)
                {
                    double energy = LJCluster.CalculateEnergy(p.positions, N);
                    if (energy == double.MaxValue) break;
                    p.UpdatePBest(energy);
                }

                UpdateLBest(lbest, lbestValue);

                for (int i = 0; i < swarmSize; i++)
                {
                    particles[i].CalculateVelocity(lbest[i]);
                    particles[i].UpdatePosition();
                }

                UpdateGBest();
            }

            PrintResults();
        }

        private void PrintResults()
        {
            Console.WriteLine();
            Console.WriteLine($"Number of evaluations: {LJCluster.numEvaluations}");
            Console.WriteLine($"Minimum energy: {gbestValue.ToString("F6", CultureInfo.InvariantCulture)}");
            Console.WriteLine("OOptimal arrangement of atoms:");

            for (int i = 0; i < N; i++)
            {
                double x = gbestPos[i * 3];
                double y = gbestPos[i * 3 + 1];
                double z = gbestPos[i * 3 + 2];
                Console.WriteLine($"  Atom {i,2}: ({x,10:F6}, {y,10:F6}, {z,10:F6})");
            }
        }
    }