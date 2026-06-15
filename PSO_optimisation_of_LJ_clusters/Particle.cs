namespace PSO_optimisation_of_LJ_clusters;

class Particle
{
    public double[] positions;
    public double[] velocity;
    public double[] pbestPos;
    public double   pbestValue = double.MaxValue;

    private const double W   = 0.7298;
    private const double Fi1 = 1.496;
    private const double Fi2 = 1.496;

    private readonly int     dim;
    private readonly Random  rng;

    public Particle(int N, Random rng)
    {
        this.dim = N * 3;
        this.rng = rng;

        positions = new double[dim];
        velocity  = new double[dim];
        pbestPos  = new double[dim];

        for (int i = 0; i < dim; i++)
        {
            positions[i] = RandomNonZero(-1.0, 1.0);
            velocity[i]  = rng.NextDouble() * 2.0 - 1.0;
        }

        Array.Copy(positions, pbestPos, dim);
        pbestValue = double.MaxValue;
    }

    private double RandomNonZero(double min, double max)
    {
        double val;
        do { val = rng.NextDouble() * (max - min) + min; }
        while (val == 0.0);
        return val;
    }

    public void UpdatePBest(double currentEnergy)
    {
        if (currentEnergy < pbestValue)
        {
            pbestValue = currentEnergy;
            Array.Copy(positions, pbestPos, dim);
        }
    }

    public void CalculateVelocity(double[] guidePos)
    {
        for (int i = 0; i < dim; i++)
        {
            double r1 = rng.NextDouble();
            double r2 = rng.NextDouble();

            velocity[i] = W   * velocity[i]
                          + Fi1 * r1 * (pbestPos[i]  - positions[i])
                          + Fi2 * r2 * (guidePos[i]  - positions[i]);
        }
    }

    public void UpdatePosition()
    {
        for (int i = 0; i < dim; i++)
            positions[i] += velocity[i];
    }
}
