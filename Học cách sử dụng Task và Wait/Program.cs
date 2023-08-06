using System;

namespace MyApp
{
    // tạo lớp Program
    public class Program
    {
        // tạo hàm thông báo
        // tham số thứ nhất: số lần lặp n
        // tham số thứ hai: một dòng dữ liệu text
        // tham số thứ ba: dùng để cài đặt màu sắc cho chữ
        public static void ThongBao(int n, string text, ConsoleColor dt_color)
        {
            for (int i = 1; i <= n; i++)
            {
                lock(Console.Out){
                    // bây giờ tôi muốn câu lệnh
                    // Console.ForegroundColor = dt_color;
                    // không bị sử dụng bởi nhiều luồng
                    // thì tôi sẽ dùng lock để khóa nó lại

                    // cài đặt màu sắc
                    Console.ForegroundColor = dt_color;

                    Console.WriteLine($"{i} ");
                    Thread.Sleep(1000);

                    // đặt lại màu sắc
                    Console.ResetColor();
                }
            }

            lock(Console.Out){
                Console.ForegroundColor = dt_color;

                // tôi đang viết code
                // theo kiểu nội suy $"{tên_biến}"
                Console.WriteLine($"{text}");

                // đặt lại màu sắc
                Console.ResetColor();
            }
        }

        public static void Main(string[] args)
        {
            Console.Clear();

            // tạo đối tượng
            // có kiểu tác vụ
            Task dt1 = new Task(() => {
                ThongBao(5, "Tac vu 1", ConsoleColor.Red);
            });

            Task dt2 = new Task((object obj) => {
                // cái obj
                // sẽ hứng cái chuỗi "Tac vu 2"

                // bây giờ, tôi tạo biến str
                // hứng dữ liệu của obj
                // nhưng tôi sẽ ép kiểu sang string
                string str = (string)obj;

                // gọi hàm thông báo
                ThongBao(5, str, ConsoleColor.Green);

            }, "Tac vu 2");


            // để bảo cái đối tượng tác vụ
            // cho nó chạy
            // thì phải gọi phương thức Start()
            dt1.Start();        // đây là luồng chạy số 2
            dt2.Start();        // đây là luồng chạy số 3

            // bây giờ, tôi muốn luồng số 2 và 3 chạy xong
            // thì tôi mới cho chạy luồng chính
            // thì phải sử dụng phương thức Wait()
            dt1.Wait();
            dt2.Wait();

            // đây là luồng chính
            ThongBao(5, "Tac vu 0", ConsoleColor.Blue);
        }
    }
}