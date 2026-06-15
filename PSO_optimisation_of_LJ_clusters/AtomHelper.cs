namespace PSO_optimisation_of_LJ_clusters;

static class AtomHelper
{
    public static double Distance(double[] positions, int i, int j)
    {
        double dx = positions[i * 3]     - positions[j * 3];
        double dy = positions[i * 3 + 1] - positions[j * 3 + 1];
        double dz = positions[i * 3 + 2] - positions[j * 3 + 2];
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}