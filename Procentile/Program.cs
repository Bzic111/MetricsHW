
//Dictionary<int,int> rayProcentile = new(1000);
List<double> rayList = new(123);
double[] ray = new double[123];
Random rnd = new();


for (int i = 0; i < ray.Length; i++)
{
    ray[i] = (double)rnd.Next(0, 99) + (double)((double)rnd.Next(1, 10) / 100);
    rayList.Add(ray[i]);
    rnd.Next(rnd.Next(rnd.Next()));
}
foreach (var item in ray)
{
    Console.Write($"{item} | ");
}
Console.WriteLine();
rayList.Sort();
foreach (var item in rayList)
{
    Console.Write($"{item} | ");
}
Console.WriteLine();
                            //n = (P/100) x N

double procentile = 90;             // P
double rayCount = ray.Length;  // N
double n = procentile / 100D * rayCount;
Console.WriteLine($"{n}");
    Console.WriteLine($"{procentile} = {rayList[(int)n]}");
for (int i = 0; i < ray.Length; i++)
{
    if (ray[i] > rayList[(int)n])
    {
        Console.WriteLine($"Procentile {procentile} = {rayList[(int)n]} == {i} | {ray[i]}");
    }
}
foreach (var item in ray)
{
}
//for (int i = 0; i < ray.Length; i++)
//{
//    //rayProcentile.Add(procentile, rayList[procentile / 100 * rayCount]);
//}