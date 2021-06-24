using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.StructureAlgorithm.Project.Algorithm
{
    public static class SortDemo
    {
        public static void Show()
        {
            int[] array = new int[10];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Random(i + DateTime.Now.Millisecond).Next(100, 999);
            }
            Console.WriteLine("before BubbleSort");
            array.Show();
            Console.WriteLine("start BubbleSort");
            //array.BubbleSort();
            //array.BubbleSortLow();

            //array.SelectionSort();

            array.InsertionSort();
            Console.WriteLine("  end BubbleSort");
            array.Show();
        }

        #region 冒泡排序
        /// <summary>
        /// 冒泡排序
        /// 先挑最大的 摆在最后面
        /// 先挑最小的 排在最前面？
        /// </summary>
        /// <param name="arr"></param>
        public static void BubbleSort(this int[] arr)
        {

            int temp;
            for (int outer = 0; outer < arr.Length - 1; outer++)
            {
                for (int inner = 0; inner < arr.Length - 1 - outer; inner++)
                {
                    if (arr[inner] > arr[inner + 1])
                    {
                        temp = arr[inner + 1];
                        arr[inner + 1] = arr[inner];
                        arr[inner] = temp;

                    }
                }
                arr.Show();
            }


            #region
            //int temp;
            //for (int outer = arr.Length; outer > 1; outer--)
            //{
            //    for (int inner = 0; inner <outer - 1; inner++)
            //    {
            //        if (arr[inner] > arr[inner + 1])
            //        {
            //            temp = arr[inner];
            //            arr[inner] = arr[inner + 1];
            //            arr[inner + 1] = temp;
            //        }
            //    }
            //    arr.Show();
            //}
            #endregion
        }
        public static void BubbleSortLow(this int[] arr)
        {
            int temp;
            for (int outer = 0; outer < arr.Length - 1; outer++)
            {
                for (int inner = 0; inner < arr.Length - 1 - outer; inner++)
                {
                    if (arr[inner] < arr[inner + 1])
                    {
                        temp = arr[inner + 1];
                        arr[inner + 1] = arr[inner];
                        arr[inner] = temp;

                    }
                }
                arr.Show();
            }

            //int temp;
            //for (int outer = 0; outer < arr.Length - 1; outer++)
            //{
            //    for (int inner = outer + 1; inner < arr.Length; inner++)
            //    {
            //        if (arr[outer] < arr[inner])
            //        {
            //            temp = arr[outer];
            //            arr[outer] = arr[inner];
            //            arr[inner] = temp;

            //        }
            //    }
            //    arr.Show();
            //}
        }

        #endregion

        #region 选择排序

        public static void SelectionSort(this int[] arr)
        {
            int min, temp;
            for (int outer = 0; outer < arr.Length; outer++)
            {
                min = outer;
                for (int inner = outer + 1; inner < arr.Length; inner++)
                {
                    if (arr[min] > arr[inner])
                    {
                        min = inner;
                    }
                }
                temp = arr[outer];
                arr[outer] = arr[min];
                arr[min] = temp;
                arr.Show();
            }
        }


        #endregion

        #region 插入排序
        public static void InsertionSort(this int[] arr)
        {
            arr =new int[5]{ 2,3,5,4,1};
            int inner, temp;
            for (int outer = 1; outer < arr.Length; outer++)
            {
                temp = arr[outer];
                inner = outer;
                while (inner > 0 && arr[inner - 1] > temp)
                {
                    arr[inner] = arr[inner - 1];
                    inner--;
                    arr.Show();
                }
                arr[inner] = temp;
               

            }

        } 
        #endregion

        private static void Show(this int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item.ToString() + " ");
            }
            Console.WriteLine();
        }

    }
}
