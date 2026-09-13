namespace PSO_optimisation_of_LJ_clusters;

class Program
{
    static void Main(string[] args)
    {
        int N = 57;
        int M = 40;


        for (int i = 0; i < 30; i++)
        {
            LJCluster.numEvaluations = 0;
            PSO pso = new PSO(M, N);
            pso.RunRingTopology();
        }
        
    }
}