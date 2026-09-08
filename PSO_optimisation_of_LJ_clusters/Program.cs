namespace PSO_optimisation_of_LJ_clusters;

class Program
{
    static void Main(string[] args)
    {
        int N = 7;
        int M = 40;

        PSO pso = new PSO(M, N);

        pso.RunStarTopology();
    }
}