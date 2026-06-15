namespace PSO_optimisation_of_LJ_clusters;

static class LJCluster
{
    public static int numEvaluations = 0;
    public const  int MAX_EVALUATIONS = 1_000_000;

    public static double LJEnergy(double r)
    {
        double r2  = r * r;
        double r6  = r2 * r2 * r2;
        double r12 = r6 * r6;
        return 4.0 * (1.0 / r12 - 1.0 / r6);
    }

    public static double CalculateEnergy(double[] positions, int N)
    {
        if (numEvaluations >= MAX_EVALUATIONS)
        {
            return double.MaxValue;
        }

        double totalEnergy = 0.0;

        for (int i = 0; i < N - 1; i++)
        {
            for (int j = i + 1; j < N; j++)
            {
                double r = AtomHelper.Distance(positions, i, j);
                totalEnergy += LJEnergy(r);
            }
        }

        numEvaluations++;
        return totalEnergy;
    }
}
