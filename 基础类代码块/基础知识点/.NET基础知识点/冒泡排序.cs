int[] x = { 6, 2, 4, 1, 5, 9 };
for (int i = 0; i < x.Length; i++)
{
    for (int j = i; j < x.Length; j++)
    {
        if (x[i] > x[j])
        {
            int temp = x[i];
            x[i] = x[j];
            x[j] = temp;
        }
    }
}
foreach (var item in x)
{
    Console.WriteLine(item);
}
-----------------------------------------------------------------------------------------------------
for (int j = 0; j < nums.Length - 1; j++)
{
    for (int i = 0; i < nums.Length - 1 -j; i++)
    {
        if (nums[i] > nums[i + 1])
        {
            int temp = nums[i];
            nums[i] = nums[i + 1];
            nums[i + 1] = temp;
        }
    }
}
-----------------------------------------------------------------------------------------------
 int [] array = new int [*] ;
 int temp = 0 ;
 for (int i = 0 ; i < array.Length - 1 ; i++)
 {
  for (int j = i + 1 ; j < array.Length ; j++)
  {
   if (array[j] < array[i])
   {
    temp = array[i] ;
    array[i] = array[j] ;
    array[j] = temp ;
   }
  }
 }