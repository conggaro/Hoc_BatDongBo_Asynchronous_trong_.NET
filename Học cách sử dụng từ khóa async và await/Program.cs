using System;

namespace MyApp
{
    public class Program
    {
        // tạo hàm trả về kiểu Task
        public static async Task TacVu_ThongBao(int n, string text, ConsoleColor color)
        {
            // tạo đối tượng Task
            Task dt = new Task(() => {
                for (int i = 1; i <= n; i++)
                {
                    lock (Console.Out)
                    {
                        Console.ForegroundColor = color;
                        Console.WriteLine($"{i}");
                        Thread.Sleep(1000);
                        Console.ResetColor();
                    }
                }

                lock (Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{text}");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
            });

            // gọi phương thức Start()
            // để cho đối tượng chạy tác vụ
            dt.Start();

            // khi chạy xong 
            // thì mới cho thực hiện câu lệnh tiếp theo
            // thì dùng từ khóa await
            // từ khóa await còn dùng thay return luôn
            await dt;
        }


        // tạo hàm Task<T>
        // để trả về kiểu dữ liệu string
        public static Task<string> Lay_Ten(string text)
        {
            // tạo đối tượng
            Task<string> dt = new Task<string>((object obj) => {
                // obj thì hứng dữ liệu của biến text
                // còn str thì hứng dữ liệu của biến obj
                string str = (string)obj;

                // tìm vị trí
                // của khoảng trắng đầu tiên
                // từ phải sang trái
                int vi_tri = str.LastIndexOf(" ");

                // tạo biến độ dài tên
                int do_dai = (str.Length - 1) - vi_tri;

                string ten = str.Substring(vi_tri + 1, do_dai);

                return ten;
            }, text);

            // gọi hàm Start()
            // để chạy đối tượng
            dt.Start();

            // dùng từ khóa await
            // để chắc chắn rằng đã chạy xong
            // thì mới trả về đối tượng dt
            return dt;
        }


        public static void Main(string[] args)
        {
            Console.Clear();

            // tạo đối tượng
            Task dt1 = TacVu_ThongBao(5, "DONE!", ConsoleColor.Red);

            Task<string> dt2 = Lay_Ten("Nguyen Van An");

            // gọi hàm tác vụ thông báo
            TacVu_ThongBao(5, dt2.Result, ConsoleColor.Blue);

            for (int i = 1; i <= 5; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i}");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
            }

            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("DONE!");
                Console.ResetColor();
            }
        }
    }
}