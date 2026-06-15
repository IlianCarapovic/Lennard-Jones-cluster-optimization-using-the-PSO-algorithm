namespace PSO_optimisation_of_LJ_clusters;

class Program
{
    static void Main(string[] args)
    {
        int N = 45;
        int swarmSize = 40;

        PSO pso = new PSO(swarmSize, N);

        pso.RunStarTopology();
    }
}