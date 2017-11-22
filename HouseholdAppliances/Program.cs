using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace HouseholdAppliances
{
    class Program
    {
        static Cart cart = new Cart();
        static Goods goodss = new Goods();
        static Category categoryes = new Category();
        static User users = new User();
        static Order orders = new Order();
        static OrderDetails orderDetailss = new OrderDetails();
        static Promo promo = new Promo();
        static IDB obj;
        static UserLine userName = null;
        delegate void GetMethod();
        static Settings set = new Settings();
        static void Main(string[] args)
        {
            Console.Title = "Интернет-магазин бытовой техники";
            set = set.Deserialize();
            users.Deserialize(set.FileUser);
            categoryes.Deserialize(set.FileCategory);
            goodss.Deserialize(set.FileGoods);
            orders.Deserialize(set.FileOrder);
            orderDetailss.Deserialize(set.FileOrderDetails);
            promo.Deserialize(set.FilePromo);
            Console.BackgroundColor = ConsoleColor.White;
            string item = "";
            string itemView = "";
            string searchLine = "";
            while (item != "6")
            {
                Menu();
                item = Console.ReadLine();
                switch (item)
                {
                    case "1":
                        itemView = null;
                        while (itemView != "4")
                        {
                            ViewProducts();
                            itemView = Console.ReadLine();
                            switch (itemView)
                            {
                                case "1":

                                    ListGoods(goodss.Lines.ToList());
                                    break;
                                case "2":
                                    ListCategory();
                                    int categoryId = IntRead();

                                    ListGoods(goodss.GoodsInCategory(categoryId));
                                    break;
                                case "3":
                                    Search();
                                    searchLine = Console.ReadLine();
                                    ListGoods(goodss.SearchGoods(searchLine));
                                    break;
                                case "4":
                                    break;
                                default:
                                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        
                        break;
                        
                    case "2":
                        Basket();
                        break;
                    case "3":
                        Registration(); 
                        Console.ReadKey();
                        break;
                    case "4":
                        if (userName == null)
                        {
                            Authorization();
                        }
                        else
                        {
                            PersonalAccount();
                        }
                        
                        break;
                    case "5":
                        AdminPanel();
                        break;
                    case "6":
                        users.Serialize(set.FileUser);
                        goodss.Serialize(set.FileGoods);
                        categoryes.Serialize(set.FileCategory);
                        orders.Serialize(set.FileOrder);
                        orderDetailss.Serialize(set.FileOrderDetails);
                        promo.Serialize(set.FilePromo);
                        break;
                    default:
                        Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                        Console.ReadKey();
                        break;
                }
            }
            
        }

        static void Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.ForegroundColor = ConsoleColor.Black;
            if (userName == null)
            {
                Console.WriteLine("Меню:\n1.Просмотр товаров\n2.Корзина\n3.Регистрация\n4.Авторизация\n5.Админ-панель\n6.Выход");

            }
            else
            {
                Console.WriteLine("Меню:\n1.Просмотр товаров\n2.Корзина\n3.Регистрация\n4.Личный кабинет\n5.Админ-панель\n6.Выход");

            }
        }

        static void ViewProducts()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Просмотр товаров");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Меню:\n1.Список всех товаров\n2.Список категорий\n3.Поиск товаров\n4.Главное меню");

        }

        static void Search()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Поиск товаров");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Строка поиска:");

        }
        static void PersonalAccount()
        {
            string exit = "";

            while (exit != "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Личный кабинет");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (UserLine u in users.Lines)
                {
                    if (u.Email == userName.Email)
                    {
                        Console.WriteLine("1.Фамилия: {0}\n2.Имя: {1}\n3.Ел. адрес: {2}\n4.Пароль: {3}\n5.Дата рождения: {4}\n6.Город: {5}\n7.Адрес: {6}\n8.Телефон: {7}", u.LastName, u.FirstName, u.Email, u.Password, u.DateOfBirth.ToLongDateString(), u.City, u.Address, u.Phone);
                
                    }
                   
                }

                Console.WriteLine("Введите пункт, который необходимо изменить (для возврата на главное меню введите 0, для просмотра истории заказов введите +, для выхода из аккаунта введите -):");
                exit = Console.ReadLine();
                if (exit == "0")
                {
                    break;
                }
                if (exit == "-")
                {
                    userName = null;
                    break;
                }
                if (exit == "+")
                {
                    History();
                    break;
                }
                
                    foreach (UserLine u in users.Lines)
                    {
                        if (u.Email == userName.Email)
                        {
                            switch (exit)
                            {
                                case"1":
                                    Console.WriteLine("Введите фамилию:");
                                    u.LastName = StringRead();
                                    break;
                                case"2":
                                    Console.WriteLine("Введите имя:");
                                    u.FirstName = StringRead();
                                    break;
                                case"3":
                                    Console.WriteLine("Введите эл. адрес:");
                                    u.Email = Email();
                                    break;
                                case"4":
                                    Console.WriteLine("Введите пароль:");
                                    u.Password = Password();
                                    break;
                                case"5":
                                    Console.WriteLine("Введите день рождения:");
                                    string day = DayRead();
                                    Console.WriteLine("Введите месяц рождения:");
                                    string month = MonthRead();
                                    Console.WriteLine("Введите год рождения:");
                                    string year = YearRead();
                                    u.DateOfBirth = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                                    break;
                                case"6":
                                    Console.WriteLine("Введите город:");
                                    u.City = StringRead();
                                    break;
                                case"7":
                                    Console.WriteLine("Введите адрес:");
                                    u.Address = Console.ReadLine();
                                    break;
                                case"8":
                                    Console.WriteLine("Введите телефон:");
                                    u.Phone = Console.ReadLine();
                                    break;
                                default:
                                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                                    Console.ReadKey();
                                    break;
                                    
                            }
                            userName = u;
                            
                            
                            break;
                        }
                    
                    
                }
                
            }
            

        }

        static void History()
        {
            string exit = "";
            bool error = true;
            while (exit != "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("История заказов");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (OrderLine o in orders.Lines)
                {
                    if (o.UserId == userName.UserId)
                    {
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Код: {0}\n\nТовары:", o.OrderId);
                        foreach (OrderDetailsLine od in orderDetailss.Lines)
                        {
                            if (o.OrderId == od.OrderId)
                                Console.WriteLine("Название: {0} \nКоличество: {1}\n", od.OrderGoods.GoodsName, od.OrderQuantity);
                        }
                        Console.WriteLine("Дата оформления заказа: {0}\nСтатус: {1}\nТелефон: {2}\nЦена: {3} грн", o.DateOrder.ToLongDateString(), o.OrderStatus, o.Phone, o.Price);

                    }
                    
                }
                Console.WriteLine("Введите код заказа, от которого хотите отказаться(для выхода введите 0):");
                exit = Console.ReadLine();
                if (exit == "0")
                {
                    break;
                }
                if (exit.Length == exit.Where(c => char.IsDigit(c)).Count())
                {
                    foreach (OrderLine o in orders.Lines)
                    {
                        if (o.OrderId == Int32.Parse(exit) && o.UserId==userName.UserId)
                        {
                            if (o.OrderStatus == "возврат")
                            {
                                Console.WriteLine("Заказ отменен, изменить его статус невозможно! Нажмите любую кнопку, чтобы продолжить:");
                            }
                            else if((DateTime.Now-o.DateOrder).Days<=14)
                            {
                                o.OrderStatus="возврат";
                                Console.WriteLine("Напишите причину отказа:");
                                o.ReasonFailure = Console.ReadLine();
                                Console.WriteLine("Заказ успешно отменен! Нажмите любую кнопку, чтобы продолжить:");
                            }
                            else
                            {
                                Console.WriteLine("Заказ можно отменить только в первые две недели после его оформления, по этой причине текущий заказ невозможно удалить! Нажмите любую кнопку, чтобы продолжить:");
                            }
                            
                            error = false;
                            break;
                        }
                    }
                }
                if (error == true)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                }
                error = true;
                Console.ReadKey();


                

            }
               
        }
        

        static void AdminPanel()
        {
            Dictionary<int, GetMethod> methods = new Dictionary<int, GetMethod>(5);
            methods.Add(1, AdminCategory);
            methods.Add(2, AdminGoods);
            methods.Add(3, ListUsers);
            methods.Add(4, ListOrder);
            methods.Add(5, ListPromo); 
            
            string login = null;
            string exit = "";
            string pass="";
            while (login == null && exit == "")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Админ-панель");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Введите логин:");
                login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                pass = Password();
                if (login != set.AdminLogin || pass!=set.AdminPassword)
                    {
                        login = null ;
                        Console.WriteLine("Логин или пароль указан не верно\nЕсли хотите вернуться в главное меню введите 0, если хотите продолжить нажмите любую кнопку");
                        if (Console.ReadLine() == "0")
                        {
                            exit = "e";
                            break;
                        }
                    }
                
            }
            if (exit == "")
            {
                
                int itemAdmin = 0;
                while (itemAdmin != 6)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Интернет магазин бытовой техники");
                    Console.WriteLine("Админ-панель");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Меню:\n1.Список категорий\n2.Список товаров\n3.Список пользователей\n4.Список заказов\n5.Список промо-кодов\n6.Выйти из админ-панели");
                    itemAdmin=IntRead();
                    if (itemAdmin<6)
                    {
                        methods[itemAdmin].Invoke();
                    }
                    else if (itemAdmin!=6)
                    {
                         Console.WriteLine("Неизвестный символ! Нажмите любую кнопку для продолжения:");
                            Console.ReadKey();
                    }

                            
                    
                    

                }

            }

        }

        static void AdminCategory()
        {
            obj = categoryes;
            string command = "";
            bool error = true;
            while (command != "0")
            {
                ListCategory();
                Console.WriteLine("(для добавления новой категории введите +, для удаления категории введите -, для возврата на админ-панель введите 0)");
                command = Console.ReadLine();
                if (command == "0")
                {
                    break;
                }
                if (command == "+")
                {

                    AddCategory();
                    
                    break;
                }
                if (command == "-")
                {
                    Console.WriteLine("Введите код категории, которую хотите удалить:");
                    int id = IntRead();
                    Console.WriteLine("При удалении категории, будут удалены все товары в ней, все заказы и промо-акции  с этими товарами! Для отмены введите 0, для подтверждения удаления - любой символ:");
                    if (Console.ReadLine() != "0")
                    {
                        int i = 0, j = 0, k = 0, q = 0;
                        while ( i<goodss.Lines.Count())
                        {
                            if (goodss.Lines.ElementAt(i).CategoryId == id)
                            {
                                while (j < promo.Lines.Count())
                                {
                                    while(k<promo.Lines.ElementAt(j).GoodsPromo.Count)
                                    {
                                        if (promo.Lines.ElementAt(j).GoodsPromo.ElementAt(k).GoodsId == goodss.Lines.ElementAt(i).GoodsId)
                                        {
                                            promo.Lines.ElementAt(j).GoodsPromo.RemoveAt(k);
                                        }
                                        else
                                        {
                                            k++;
                                        }
                                    }
                                    if (promo.Lines.ElementAt(j).GoodsPromo.Count == 0)
                                    {
                                        promo.RemoveLine(promo.Lines.ElementAt(j));
                                    }
                                    else
                                    {
                                        j++;
                                    }
                                    
                                }
                                j = 0;
                                k = 0;
                                while (j < orderDetailss.Lines.Count())
                                {
                                    if (orderDetailss.Lines.ElementAt(j).OrderGoods.GoodsId == goodss.Lines.ElementAt(i).GoodsId)
                                    {
                                        while (k < orders.Lines.Count())
                                        {
                                            if (orders.Lines.ElementAt(k).OrderId == orderDetailss.Lines.ElementAt(j).OrderId)
                                            {
                                                while (q < orders.Lines.ElementAt(k).products.Count)
                                                {
                                                    if (orders.Lines.ElementAt(k).products.ElementAt(q).OrderGoods.GoodsId == goodss.Lines.ElementAt(i).GoodsId)
                                                    {
                                                        orders.Lines.ElementAt(k).products.RemoveAt(q);
                                                    }
                                                    else
                                                    {
                                                        q++;
                                                    }
                                                }
                                                
                                            }
                                            if (orders.Lines.ElementAt(k).products.Count == 0)
                                            {
                                                orders.RemoveLine(orders.Lines.ElementAt(k));
                                            }
                                            else
                                            {
                                                k++;
                                            }
                                            
                                        }
                                        orderDetailss.RemoveLine(orderDetailss.Lines.ElementAt(j));
                                        
                                    }
                                    else
                                    {
                                        j++;
                                    }
                                    

                                }
                                goodss.RemoveLine(goodss.Lines.ElementAt(i));
                            }
                            else
                            {
                                i++;
                            }
                        }
                        categoryes.RemoveLine(categoryes.Lines.Where(c => c.CategoryId == id).FirstOrDefault());
                        Console.WriteLine("Категория успешно удалена");
                        Console.ReadKey();
                    }
                    
                   
                    break;
                }
                if (command.Length == command.Where(c => char.IsDigit(c)).Count()&&command!="")
                {
                    Console.WriteLine("Введите новое название категории:");
                    string name = Console.ReadLine();
                    for (int i = 0; i < categoryes.Lines.Count(); i++)
                    {
                        if (Int32.Parse(command) == categoryes.Lines.ElementAt(i).CategoryId)
                        {
                            categoryes.Lines.ElementAt(i).CategoryName = name;
                            Console.WriteLine("Категория успешно изменена! Нажмите любую кнопку для продолжения:");
                            error = false;
                            break;
                        }
                    }

                    
                }
                if (error == true)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                }
                error = true;
                Console.ReadKey();
            }
            SaveChange();
            
                
            
            
        }

        static void AdminGoods()
        {
            obj = goodss;
            string command = "";
            string id = "";
            bool error = true;
            while (command != "0")
            {
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Список товаров");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (GoodsLine g in goodss.Lines)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Код товара: {0}\nНазвание товара: {1}\nОписание: {2}\nКатегория: {3}\nЦена: {4} грн\n", g.GoodsId, g.GoodsName, g.Description,categoryes.Lines.Where(c=>c.CategoryId==g.CategoryId).FirstOrDefault().CategoryName, g.GoodsPrice);
                }

                Console.WriteLine("Введите код товара, который необходимо изменить (для выхода введите 0, для добавления товара +, для удаления -):");
                command = Console.ReadLine();
                if (command == "0")
                {
                    break;
                }
                if (command == "+")
                {
                    AddGoods();
                    continue;
                }
                if (command == "-")
                {
                    Console.WriteLine("Введите код товара, который хотите удалить:");
                    id = Console.ReadLine();
                    Console.WriteLine("При удалении товара, будут удалены все заказы и промо-акции с этими товаром! Для отмены введите 0, для подтверждения удаления - любой символ:");

                    if (Console.ReadLine() != "0")
                    {
                        int i = 0, j = 0, k = 0, q = 0;
                        while (i < goodss.Lines.Count())
                        {
                            if (goodss.Lines.ElementAt(i).GoodsId == Int32.Parse(id))
                            {
                                j = 0;
                                while (j < promo.Lines.Count())
                                {
                                    k = 0;
                                    while (k < promo.Lines.ElementAt(j).GoodsPromo.Count)
                                    {
                                        if (promo.Lines.ElementAt(j).GoodsPromo.ElementAt(k).GoodsId == goodss.Lines.ElementAt(i).GoodsId)
                                        {
                                            promo.Lines.ElementAt(j).GoodsPromo.RemoveAt(k);
                                        }
                                        else
                                        {
                                            k++;
                                        }
                                    }
                                    if (promo.Lines.ElementAt(j).GoodsPromo.Count == 0)
                                    {
                                        promo.RemoveLine(promo.Lines.ElementAt(j));
                                    }
                                    else
                                    {
                                        j++;
                                    }

                                }
                                j = 0;
                                while (j < orderDetailss.Lines.Count())
                                {
                                    if (orderDetailss.Lines.ElementAt(j).OrderGoods.GoodsId == goodss.Lines.ElementAt(i).GoodsId)
                                    {
                                        k = 0;
                                        while (k < orders.Lines.Count())
                                        {
                                            if (orders.Lines.ElementAt(k).OrderId == orderDetailss.Lines.ElementAt(j).OrderId)
                                            {
                                                q = 0;
                                                while (q < orders.Lines.ElementAt(k).products.Count)
                                                {
                                                    if (orders.Lines.ElementAt(k).products.ElementAt(q).OrderGoods.GoodsId == goodss.Lines.ElementAt(i).GoodsId)
                                                    {
                                                        orders.Lines.ElementAt(k).products.RemoveAt(q);
                                                    }
                                                    else
                                                    {
                                                        q++;
                                                    }
                                                }

                                            }
                                            if (orders.Lines.ElementAt(k).products.Count == 0)
                                            {
                                                orders.RemoveLine(orders.Lines.ElementAt(k));
                                            }
                                            else
                                            {
                                                k++;
                                            }

                                        }
                                        orderDetailss.RemoveLine(orderDetailss.Lines.ElementAt(j));

                                    }
                                    else
                                    {
                                        j++;
                                    }


                                }
                                goodss.RemoveLine(goodss.Lines.ElementAt(i));
                                break;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        Console.WriteLine("Товар успешно удален");
                        Console.ReadKey();

                    }
                }
                else
                {
                    id = command;
                }
                if (id.Length == id.Where(c => char.IsDigit(c)).Count()&&id!="")
                {
                    foreach (GoodsLine g in goodss.Lines)
                    {

                        if (g.GoodsId == Int32.Parse(id))
                        {
                            
                                ChangeGoods(g);
                                error = false;
                                break;
                            
                            
                        }
                    }
                }
                
                if (error == true)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                    Console.ReadKey();
                }
                error = true;
                
            }
            SaveChange();
        }

        static void ListPromo()
        {
            obj =  promo;
            string command = "";
            string id = "";
            bool error = true;
            while (command != "0")
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Список промо-кодов");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (PromoLine p in promo.Lines)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Код промо: {0}\nПромо-код: {1}\nСкидка: {2}\nТовары:", p.PromoId, p.PromoCode, p.PromoDiscount);
                    foreach(GoodsLine g in p.GoodsPromo)
                    {
                        Console.WriteLine(g.GoodsName);
                    
                    }             
                }
                Console.WriteLine("Введите код  промо, который необходимо изменить (для выхода введите 0, для добавления промо +, для удаления -):");
                command = Console.ReadLine();
                if (command == "0")
                {
                    break;
                }
                if (command == "+")
                {
                    AddPromo();
                    continue;
                }
                if (command == "-")
                {
                    Console.WriteLine("Введите код промо, который хотите удалить:");
                    id = Console.ReadLine();


                }
                else
                {
                    id = command;
                }
                if (id.Length == id.Where(c => char.IsDigit(c)).Count() && id != "")
                {
                    foreach (PromoLine p in promo.Lines)
                    {

                        if (p.PromoId == Int32.Parse(id))
                        {
                            if (command == "-")
                            {
                                promo.RemoveLine(p);
                                Console.WriteLine("Промо успешно удален! Нажмите любую кнопку, чтобы продолжить:");
                                error = false;
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                ChangePromo(p);
                                error = false;
                                break;
                            }

                        }
                    }
                }

                if (error == true)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                    Console.ReadKey();
                }
                error = true;

            }
            SaveChange();
        }
         
        static void ChangeGoods(GoodsLine g)
        {
            string command = "";
            while (command != "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Изменение товара '{0}'", g.GoodsName);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("1.Название товара: {0}\n2.Описание: {1}\n3.Номер категории: {2}\n4.Цена: {3} грн",  g.GoodsName, g.Description, categoryes.Lines.Where(c=>c.CategoryId==g.CategoryId).FirstOrDefault().CategoryName, g.GoodsPrice);
                Console.WriteLine("Введите пункт, который хотите изменить (для отмены введите 0)");
                
                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.WriteLine("Введите название товара:");
                        g.GoodsName = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Введите описание товара:");
                        g.Description = Console.ReadLine();
                        break;
                    case "3":
                        foreach (CategoryLine c in categoryes.Lines)
                        {

                            Console.WriteLine("{0}. {1}", c.CategoryId, c.CategoryName);
                        }
                        Console.WriteLine("Введите код необходимой категории:");
                        int id = IntRead();
                        if(categoryes.Lines.Where(c=>c.CategoryId==id).Count()!=0 )
                        {
                            g.CategoryId = id;
                        }
                        else
                        {
                            Console.WriteLine("Такой категории не существует! Нажмите любую кнопку для продолжения:");
                            Console.ReadKey();
                        }
                        break;
                    case "4":
                        Console.WriteLine("Введите цену товара:");
                        g.GoodsPrice = IntRead();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                        Console.ReadKey();
                        break;
                }
            }
            
        }

        static void ChangePromo(PromoLine p)
        {
            string command = "";
            while (command != "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Изменение промо-кода '{0}'", p.PromoCode);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("1.Промо-код: {0}\n2.Скидка: {1}\n3.Товары:", p.PromoCode, p.PromoDiscount);
                foreach (GoodsLine g in p.GoodsPromo)
                {
                    Console.WriteLine(g.GoodsName);
                }
                Console.WriteLine("\nВведите пункт, который хотите изменить (для отмены введите 0)");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.WriteLine("Введите промо-код:");
                        p.PromoCode = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Введите скидку:");
                        p.PromoDiscount = IntRead();
                        break;
                    case "3":
                        ListPromoGoods(p);
                        break;
                   case "0":
                        break;
                    default:
                        Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                        Console.ReadKey();
                        break;
                }
            }

        }

        static void ListPromoGoods(PromoLine p)
        {
            string command = "";
            bool error = true;
            while (command != "0")
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Список товаров в промо");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (GoodsLine g in p.GoodsPromo)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Код товара: {0}\nНазвание товара: {1}\nОписание: {2}\nКатегория: {3}\nЦена: {4} грн\n", g.GoodsId, g.GoodsName, g.Description, categoryes.Lines.Where(c=>c.CategoryId==g.CategoryId).FirstOrDefault().CategoryName, g.GoodsPrice);
                }

                Console.WriteLine("Введите код товара, который необходимо удалить из списка промо (для выхода введите 0, для добавления товара +):");
                command = Console.ReadLine();
                if (command == "0")
                {
                    break;
                }
                if (command == "+")
                {
                    int ex = -1;
                    foreach (GoodsLine g in goodss.Lines)
                    {
                        Console.WriteLine("{0}. {1}", g.GoodsId, g.GoodsName);
                    }
                    while (ex != 0)
                    {

                        Console.WriteLine("Введите код товара, который будет иметь промо-код (для отмены введите 0):");
                        ex = IntRead();
                        if (ex == 0)
                        {
                            break;
                        }
                        if (goodss.Lines.Where(g => g.GoodsId == ex).Count() != 0)
                        {
                            p.GoodsPromo.Add(goodss.Lines.Where(g => g.GoodsId == ex).FirstOrDefault());
                            Console.WriteLine("Товар добавлен в список промо-кода! Нажмите любую кнопку для продолжения:");
                            Console.ReadKey();

                        }
                        else
                        {
                            Console.WriteLine("Такого товара не существует! Нажмите любую кнопку для продолжения:");
                            Console.ReadKey();
                        }
                    }


                    continue;
                }
                if (command.Length == command.Where(c => char.IsDigit(c)).Count() && command != "")
                {
                    foreach (GoodsLine g in p.GoodsPromo)
                    {

                        if (g.GoodsId == Int32.Parse(command))
                        {
                            p.GoodsPromo.Remove(g);
                            Console.WriteLine("Товар успешно удален из списка промо! Нажмите любую кнопку, чтобы продолжить:");
                            error = false;
                            Console.ReadKey();
                            break;
                            

                        }
                    }
                }

                if (error == true)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                    Console.ReadKey();
                }
                error = true;

            }
        }
        static void AddGoods()
        {
            obj = goodss;
            obj.AddItem();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Добавление товара");
            Console.ForegroundColor = ConsoleColor.Black;
            
            Console.WriteLine("Введите название нового товара:");
            goodss.Lines.Last().GoodsName = Console.ReadLine();
            Console.WriteLine("Введите описание нового товара:");
            goodss.Lines.Last().Description = Console.ReadLine();
            foreach (CategoryLine c in categoryes.Lines)
            {

                Console.WriteLine("{0}. {1}", c.CategoryId, c.CategoryName);
            }
            int id = 0;
            while (id==0||categoryes.Lines.Where(c => c.CategoryId == id).Count() == 0)
            {
                Console.WriteLine("Введите код необходимой категории:");
                id = IntRead();
                if (categoryes.Lines.Where(c => c.CategoryId == id).Count() != 0)
                {
                    goodss.Lines.Last().CategoryId = id;
                    break;
                }
                else
                {
                    Console.WriteLine("Такой категории не существует! Нажмите любую кнопку для продолжения:");
                    Console.ReadKey();
                }
            }
            
            Console.WriteLine("Введите цену нового товара:");
            goodss.Lines.Last().GoodsPrice = IntRead();
            Console.WriteLine("Товар успешно добавлен! Нажмите любую кнопку, чтобы продолжить:");
            Console.ReadKey();
        }

        static void AddPromo()
        {
            promo.AddItem();
            List<GoodsLine> goods = new List<GoodsLine>();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Добавление промо");
            Console.ForegroundColor = ConsoleColor.Black;
            
            Console.WriteLine("Введите промо-код:");
            promo.Lines.Last().PromoCode = Console.ReadLine();
            Console.WriteLine("Введите скидку в процентах:");
            promo.Lines.Last().PromoDiscount = IntRead();
            int ex= -1;
            foreach (GoodsLine g in goodss.Lines)
            {
                Console.WriteLine("{0}. {1}", g.GoodsId, g.GoodsName);
            }
            while(ex!=0) 
            {
                
                Console.WriteLine("Введите код товара, который будет иметь промо-код (для отмены введите 0):");
                ex = IntRead();
                if (ex==0){
                    break;
                }
                if (goodss.Lines.Where(g => g.GoodsId == ex).Count() != 0)
                    {
                        goods.Add(goodss.Lines.Where(g => g.GoodsId == ex).FirstOrDefault());
                        Console.WriteLine("Товар добавлен в список промо-кода! Нажмите любую кнопку для продолжения:");
                        Console.ReadKey();

                    }
                else
                    {
                        Console.WriteLine("Такого товара не существует! Нажмите любую кнопку для продолжения:");
                        Console.ReadKey();
                    }
                }
                
            
            promo.Lines.Last().GoodsPromo = goods;
            Console.WriteLine("Промо успешно добавлен! Нажмите любую кнопку, чтобы продолжить:");
            Console.ReadKey();
        }

        static void ListUsers()
        {
            obj = users;
            string exit = "";
            bool error = true;
            while (exit != "0")
            {
                string access = null;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Список пользователей");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (UserLine u in users.Lines)
                {
                    Console.WriteLine("---------------------------------------");
                    access = "заблокирован";
                    if (u.Access == true)
                    {
                        access = "есть";
                    }
                    Console.WriteLine("Код: {0}\nФамилия: {1}\nИмя: {2}\nЕл. адрес: {3}\nДата рождения: {4}\nГород: {5}\nАдрес: {6}\nТелефон: {7}\nДоступ: {8}", u.UserId, u.LastName, u.FirstName, u.Email, u.DateOfBirth.ToLongDateString(), u.City, u.Address, u.Phone, access);
                }

                Console.WriteLine("Введите код пользователя, чей доступ необходимо изменить (для выхода введите 0):");
                exit =Console.ReadLine();
                if (exit == "0")
                {
                    break;
                }
                if (exit.Length == exit.Where(c => char.IsDigit(c)).Count()&&exit!="")
                {
                    foreach (UserLine u in users.Lines)
                    {
                        if (u.UserId == Int32.Parse(exit))
                        {
                            if (u.Access == true)
                            {
                                u.Access = false;
                            }
                            else
                            {
                                u.Access = true;
                            }
                            Console.WriteLine("Доступ пользователя успешно изменен! Нажмите любую кнопку, чтобы продолжить:");
                            error = false;
                            break;
                        }
                    }
                }
                if (error == true)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                }
                error = true;
                Console.ReadKey();
            }
            SaveChange();
            
            
        }

        static void ListOrder()
        {
            
            string exit = "";
            string userEmail = null;
            string status = null;
            bool error = true;
            while (exit != "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Список заказов");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (OrderLine o in orders.Lines)
                {
                    foreach (UserLine u in users.Lines)
                    {
                        if (u.UserId == o.UserId)
                        {
                            userEmail = u.Email;
                            break;
                        }
                    }
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Код: {0}\nПользователь: {1}\n\nТовары:", o.OrderId, userEmail);
                    foreach (OrderDetailsLine od in orderDetailss.Lines)
                    {
                        if (o.OrderId == od.OrderId)
                            Console.WriteLine("Название: {0} \nКоличество: {1}\n", od.OrderGoods.GoodsName, od.OrderQuantity);
                    }
                    Console.WriteLine("Дата оформления заказа: {0}\nСтатус: {1}\nТелефон: {2}\nЦена: {3} грн", o.DateOrder.ToLongDateString(), o.OrderStatus, o.Phone, o.Price);
                    if (o.OrderStatus == "возврат")
                    {
                        Console.WriteLine("Причина отказа: {0}", o.ReasonFailure);
                    
                    }    
                }

                Console.WriteLine("Введите код заказа, в котором необходимо изменить статус(для выхода введите 0):");
                exit = Console.ReadLine();
                if (exit == "0")
                {
                    break;
                }
                if (exit.Length == exit.Where(c => char.IsDigit(c)).Count() && exit!="")
                {

                    foreach (OrderLine o in orders.Lines)
                    {
                        if (o.OrderId == Int32.Parse(exit))
                        {
                            status = o.OrderStatus;
                            switch (o.OrderStatus)
                            {
                                case "заказано":
                                    o.OrderStatus = "оплачено";
                                    Console.WriteLine("Статус заказа из '{0}' на '{1}' успешно изменен! Нажмите любую кнопку, чтобы продолжить:", status,  o.OrderStatus);
                            
                                    break;
                                case "оплачено":
                                    o.OrderStatus = "поставляется";
                                    Console.WriteLine("Статус заказа из '{0}' на '{1}' успешно изменен! Нажмите любую кнопку, чтобы продолжить:", status,  o.OrderStatus);
                            
                                    break;
                                case "поставляется":
                                    o.OrderStatus = "получено";
                                    Console.WriteLine("Статус заказа из '{0}' на '{1}' успешно изменен! Нажмите любую кнопку, чтобы продолжить:", status,  o.OrderStatus);
                            
                                    break;
                                case "возврат":
                                    o.OrderStatus = "вернули";
                                    Console.WriteLine("Статус заказа из '{0}' на '{1}' успешно изменен! Нажмите любую кнопку, чтобы продолжить:", status,  o.OrderStatus);
                            
                                    break; 
                                default:
                                    Console.WriteLine("Статус заказа изменить невозможно! Нажмите любую кнопку, чтобы продолжить:");
                                    break;
                            
                            }
                            error = false;
                            break;
                        }
                    }
                }
                if (error == true)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                }
                error = true;
                Console.ReadKey();
            }
        }
        static void AddCategory()
        {
            bool cat = false;
            categoryes.AddItem();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Добавление категории");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Введите название новой категории:");
            categoryes.Lines.Last().CategoryName = Console.ReadLine();
            for (int i=0; i < categoryes.Lines.Count() - 1;i++ )
            {
                if (categoryes.Lines.Last().CategoryName.ToLower() == categoryes.Lines.ElementAt(i).CategoryName.ToLower())
                {
                    cat = true;
                    break;
                }
            }
            if (cat == false)
            {
                Console.WriteLine("Категория успешно добавлена! Нажмите любую кнопку, чтобы продолжить:");
                Console.ReadKey();
            }
            else
            {
                categoryes.RemoveLine(categoryes.Lines.Last());
                Console.WriteLine("Такая категория уже существует! Нажмите любую кнопку, чтобы продолжить:");
                Console.ReadKey();
            }
            
        }
        
        static void Authorization()
        {
            string exit = "";
            string email="";
            string pass="";
            while (userName==null && exit!="e")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Авторизация");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Введите вашу ел. почту:");
                email = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                pass = Password();
                for (int i = 0; i < users.Lines.Count(); i++)
                {
                    if (users.Lines.ElementAt(i).Email == email && users.Lines.ElementAt(i).Password == pass)
                    {
                        if (users.Lines.ElementAt(i).Access == true)
                        {
                            userName = users.Lines.ElementAt(i);
                            Console.WriteLine("Добро пожаловать, {0}! Нажмите любую кнопку для продолжения:", users.Lines.ElementAt(i).FirstName);
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Этот профиль заблокирован администратором\nЕсли хотите вернуться в главное меню введите 0, если хотите продолжить нажмите любую кнопку");
                            exit = "go";
                            if (Console.ReadLine() == "0")
                            {
                                exit = "e";
                                break;
                            }
                        }
                        
                    }
                }
                if (userName == null && exit == "" )
                {
                    Console.WriteLine("Логин или пароль указан не верно\nЕсли хотите вернуться в главное меню введите 0, если хотите продолжить нажмите любую кнопку");
                    if (Console.ReadLine() == "0")
                    {
                        exit = "e";
                        break;
                    }

                }

            }
        }
        static void Basket()
        {
            string command = null;
            int quantity = -1;
            while (command != "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Корзина");
                Console.ForegroundColor = ConsoleColor.Black;
                for (int i = 0; i < cart.Lines.Count(); i++)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Код: {0}\nНазвание товара: {1}\nКоличетсво товара: {2}\nОбщая стоимость: {3}\n", cart.Lines.ElementAt(i).CartId, cart.Lines.ElementAt(i).Goods.GoodsName, cart.Lines.ElementAt(i).Quantity, cart.Lines.ElementAt(i).Goods.GoodsPrice * cart.Lines.ElementAt(i).Quantity);


                }
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Общая стоимость всей покупки: {0}", cart.ComputeTotalValue());
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Введите код товара, который хотите изменить или удалить (для выхода введите 0)");
                if (cart.Lines.Count() != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Для оформления заказа введите +:");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                
                command = Console.ReadLine();
                if (cart.Lines.Count() != 0)
                {
                    if (command == "+")
                    {
                        if (userName == null)
                        {
                            Console.WriteLine("Если у вас есть аккаунт, нажмите любую кнопку (если нет - введите +):");
                            if (Console.ReadLine() == "+")
                            {
                                Registration();
                            }
                            Authorization();
                            if (userName != null)
                            {
                                AddOrder();
                                cart.Clear();
                                Console.WriteLine("Заказ успешно оформлен! Нажмите любую кнопку:");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            AddOrder();
                            cart.Clear();
                            Console.WriteLine("Заказ успешно оформлен! Нажмите любую кнопку:");
                            Console.ReadKey();
                            break;
                        }

                        continue;

                    }
                }
                
                if (command == "0")
                {
                    break;
                }
                if (command.Count() != command.Where(c => char.IsDigit(c)).Count()||command=="")
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                    Console.ReadKey();
                    continue;
                }
                if (cart.Lines.Count()==0 || Int32.Parse(command) > cart.Lines.Last().CartId)
                {
                    Console.WriteLine("Неизвестный символ! Нажмите любую кнопку, чтобы продолжить:");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine("Введите необходимое количество этого товара (для удаления товара из корзины введите 0):");
                quantity = IntRead();
               
                if (quantity == 0)
                {
                    for (int i = 0; i < cart.Lines.Count(); i++)
                    {
                        if (Int32.Parse(command) == cart.Lines.ElementAt(i).CartId)
                        {
                            cart.RemoveLine(cart.Lines.ElementAt(i).Goods);
                            Console.WriteLine("Товар удален из корзины! Нажмите любую кнопку, чтобы продолжить:");
                            Console.ReadKey();
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < cart.Lines.Count(); i++)
                    {
                        if (Int32.Parse(command) == cart.Lines.ElementAt(i).CartId)
                        {
                            cart.Lines.ElementAt(i).Quantity = quantity;
                            Console.WriteLine("Количество товара успешно изменено! Нажмите любую кнопку, чтобы продолжить:");
                            Console.ReadKey();
                            break;
                        }
                    }
                }

            }
            

        }

        static void Registration()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Регистрация");
            Console.ForegroundColor = ConsoleColor.Black;
            users.AddItem();
            
            Console.WriteLine("Введите фамилию:");
            users.Lines.Last().LastName = StringRead();
            Console.WriteLine("Введите имя:");
            users.Lines.Last().FirstName = StringRead();
            Console.WriteLine("Введите эл. адрес:");
            users.Lines.Last().Email = Email();
            Console.WriteLine("Введите пароль:");
            users.Lines.Last().Password = Password();
            Console.WriteLine("Введите день рождения:");
            string day = DayRead();
            Console.WriteLine("Введите месяц рождения:");
            string month = MonthRead();
            Console.WriteLine("Введите год рождения:");
            string year = YearRead();
            users.Lines.Last().DateOfBirth = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
            Console.WriteLine("Введите город:");
            users.Lines.Last().City = StringRead();
            Console.WriteLine("Введите адрес:");
            users.Lines.Last().Address = Console.ReadLine();
            Console.WriteLine("Введите телефон:");
            users.Lines.Last().Phone = Console.ReadLine();
            users.Lines.Last().Access = true;
            Console.WriteLine("Вы успешно зарегестрированы. Нажмите любую кнопку, чтобы продолжить");
        }

        static void ListGoods(List<GoodsLine> list )
        {
            bool existence = false;
            string productId="-1";
            int quantity = 1;
            int cartId = 1;
            while (productId != "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Интернет магазин бытовой техники");
                Console.WriteLine("Список товаров");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (GoodsLine g in list)
                {
                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("Код товара: {0}\nНазвание товара: {1}\nОписание: {2}\nКатегория: {3}\nЦена: {4} грн\n", g.GoodsId, g.GoodsName, g.Description, categoryes.Lines.Where(c => c.CategoryId == g.CategoryId).FirstOrDefault().CategoryName, g.GoodsPrice);
                } 
               
                    Console.WriteLine("Введите код товара, который хотите добавить в корзину (для выхода введите 0):");
                    
                    productId = Console.ReadLine();
                    if (productId == "0")
                    {
                        break;
                    }
                    if (productId.Count() != productId.Where(c => char.IsDigit(c)).Count()||productId=="")
                    {
                        Console.WriteLine("Товара с таким кодом в данном списке не существует! Нажмите любую кнопку, чтобы продолжить:");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("Введите количество выбранного товара:");
                    quantity = IntRead();
                    if (cart.Lines.Count() != 0)
                    {
                        cartId = cart.Lines.Last().CartId + 1;
                    }
                    
                        foreach (GoodsLine g in list)
                        {
                            if (g.GoodsId == Int32.Parse(productId))
                            {
                                cart.AddItem(cartId, g, quantity);

                                Console.WriteLine("Товар добавлен в корзину, нажмите любую кнопку, чтобы продолжить:");
                                Console.ReadKey();
                                existence = true;
                                break;
                            }
                        }
                   
                    if (existence == false)
                    {
                        Console.WriteLine("Товара с таким кодом в данном списке не существует! Нажмите любую кнопку, чтобы продолжить:");
                        Console.ReadKey();
                    }
                    

            
            
            }
        }

        static void ListCategory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Список категорий");
            Console.ForegroundColor = ConsoleColor.Black;



            foreach (CategoryLine c in categoryes.Lines)
                {

                    Console.WriteLine("{0}. {1}", c.CategoryId,c.CategoryName);
                }
            
            Console.WriteLine("Введите номер необходимой категории:");
        }
       

        

        

        static string Email()
        {
            string result=null;
            while (result==null)
            {
                result=Console.ReadLine();
                if (result.Where(c => c=='@').Count() == 1)
                {
                    foreach (UserLine u in users.Lines)
                    {
                        if (result == u.Email)
                        {
                            Console.WriteLine("Пользователь с этим электронным адресом уже зарегистрирован");
                            result = null;
                            break;
                        }
                    }
                    continue;
                }
                else
                {
                    Console.WriteLine("Эл. адрес должен содержать символ '@'");
                    result = null;
                }
            }
            return result;
        }

        static string Password()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        
        }

        static int IntRead()
        {
            string result = null;
            while (true)
            {
                result = Console.ReadLine();
                if (result.Where(c => char.IsDigit(c)).Count() == result.Count()&& result!="")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введено не число!");
                }
            }
            return Int32.Parse(result);

        }
        static string StringRead()
        {
            string result = null;
            while (true)
            {
                result = Console.ReadLine();
                if (result.Where(c => char.IsDigit(c)).Count() != result.Count() && result != "")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Строка не должна быть пустой и не должна содержать цифр!");
                }
            }
            return result;

        }
        static string DayRead()
        {
            string result = null;
            while (true)
            {
                result = Console.ReadLine();
                if (result.Where(c => char.IsDigit(c)).Count() == result.Count() && result != "" && Int32.Parse(result) > 0 && Int32.Parse(result) < 32)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильно введеный день!");
                }
            }
            return result;

        }
        static string MonthRead()
        {
            string result = null;
            while (true)
            {
                result = Console.ReadLine();
                if (result.Where(c => char.IsDigit(c)).Count() == result.Count() && result != "" && Int32.Parse(result) > 0 && Int32.Parse(result) < 13)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильно введеный месяц!");
                }
            }
            return result;

        }
        static string YearRead()
        {
            string result = null;
            while (true)
            {
                result = Console.ReadLine();
                if (result.Where(c => char.IsDigit(c)).Count() == result.Count() && result != "" && Int32.Parse(result) > 1900 && Int32.Parse(result) < 2100)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильно введеный год!");
                }
            }
            return result;

        }
        static void AddOrder()
        {
            decimal price = 0;
            List<OrderDetailsLine> orderDetails = new List<OrderDetailsLine>();
            orders.AddItem();
            string promoCode = null;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Интернет магазин бытовой техники");
            Console.WriteLine("Оформление заказа");
            Console.ForegroundColor = ConsoleColor.Black;
            
            
            for (int i = 0; i < cart.Lines.Count();i++ )
            {
                orderDetailss.AddItem();

                orderDetailss.Lines.Last().OrderId = orders.Lines.Last().OrderId;
                orderDetailss.Lines.Last().OrderGoods = cart.Lines.ElementAt(i).Goods;
                orderDetailss.Lines.Last().OrderQuantity = cart.Lines.ElementAt(i).Quantity;
                orderDetails.Add(orderDetailss.Lines.Last());

            }
            while(promoCode!="0")
            {
                Console.WriteLine("Введите промо-код, если у вас его нет - введите 0");
                promoCode = Console.ReadLine();
                if (promoCode != "0")
                {
                    if (promo.Lines.Where(p => p.PromoCode == promoCode).Count() != 0)
                    {
                        foreach (PromoLine p in promo.Lines)
                        {
                            if (p.PromoCode == promoCode)
                            {
                                foreach (OrderDetailsLine o in orderDetails)
                                {
                                    if (p.GoodsPromo.Where(pr => pr.GoodsId == o.OrderGoods.GoodsId).Count() != 0)
                                    {
                                        price += (o.OrderGoods.GoodsPrice - (o.OrderGoods.GoodsPrice / 100 * p.PromoDiscount))*o.OrderQuantity;
                                    }
                                    else
                                    {
                                        price += o.OrderGoods.GoodsPrice*o.OrderQuantity;
                                    }
                                }
                                break;
                            }
                            
                        }
                        
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Такого промо-кода не существует! Для продолжения нажмите любую кнопку:");
                    }
                }
            }
            if (price == 0)
            {
                price = cart.ComputeTotalValue();
            }
            orders.Lines.Last().UserId = userName.UserId;
            orders.Lines.Last().products = orderDetails;
            orders.Lines.Last().DateOrder = DateTime.Today;
            orders.Lines.Last().Phone = userName.Phone;
            orders.Lines.Last().Price = price;
            Console.WriteLine("Для сохранения файла заказа в формате JSON введите j, для сохранения в формате XML введите любой другой символ:");
            
            if (Console.ReadLine() == "j")
            {
                SerializeJson(userName, orders.Lines.Last());
            }
            else
            {
                SerializeXML(userName,orders.Lines.Last());
            }
        }
        static void SerializeJson(UserLine u, OrderLine o)
        {
            string path = u.LastName + "_заказ_" + o.OrderId.ToString() + ".json";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(OrderLine));

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, o);
            }
        }

        static void SerializeXML(UserLine u, OrderLine o)
        {
            string path = u.LastName + "_заказ_" + o.OrderId.ToString()+".xml";
            XmlSerializer formatter = new XmlSerializer(typeof(OrderLine));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, o);
            }
        }
        static void SaveChange()
        {
            Console.WriteLine("Для сохранения изменений в другой файл введите + (если будет введен другой символ, то изменения сохранятся в файл, который указан в файле настроек):");
            if (Console.ReadLine() == "+")
            {
                Console.WriteLine("Введите название файла:");
                string nameFile= Console.ReadLine()+".json";
                obj.Serialize(nameFile);
                Console.WriteLine("Изменения сохранены в файл {0}. Нажмите любую кнопку для продолжения:", nameFile);
                Console.ReadKey();
                
            
            }
        }
    }
    
}
