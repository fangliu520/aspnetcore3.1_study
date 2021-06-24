using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.StructureAlgorithm.Project.Structure
{
    public class ArraryDemo
    {
        public static void Show()
        {
            {
                Console.WriteLine("***************Array******************");
                int[] intArray = new int[3];
                intArray[0] = 1;
                string[] strArray = new string[] { "123","456"};
            }
            {
                Console.WriteLine("***************多维Array******************");

                int[,] a = new int[3, 4] {
                                 {0, 1, 2, 3} ,   /*  初始化索引号为 0 的行 */
                                 {4, 5, 6, 7} ,   /*  初始化索引号为 1 的行 */
                                 {8, 9, 10, 11}   /*  初始化索引号为 2 的行 */
                                };
            }
            {
                Console.WriteLine("***************锯齿Array******************");
                int[][] a = new int[2][];
                a[0] = new int[] { 1, 2, 3 };
                a[1] = new int[] { 2 };
            }
            {
                Console.WriteLine("***************ArrayList******************");
                ArrayList arrayList = new ArrayList();
                arrayList.Add("maojindao");
                arrayList.Add("hanmin");
                arrayList.Add(22);

                //arrayList[3] = 26;
                //arrayList.RemoveAt(3);

                arrayList.Remove(22);
                {
                    ArrayList arrayList1 = new ArrayList();
                    arrayList1.Add("maojindao");
                    arrayList1.Add("Is");
                    Console.WriteLine(arrayList1.Capacity);
                    arrayList1.TrimToSize();
                    Console.WriteLine(arrayList1.Capacity);
                }
                {
                    ArrayList arrayList1 = new ArrayList(6);
                    arrayList1.Add("maojindao");
                    arrayList1.Add("Is");
                    arrayList1.Add("maojindao");
                    arrayList1.Add("Is");
                    Console.WriteLine(arrayList1.Capacity);
                    arrayList1.TrimToSize();
                    Console.WriteLine(arrayList1.Capacity);
                }
            }
        }
    }
}
