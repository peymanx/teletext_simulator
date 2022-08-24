
// Pages database - source: Linux bash fortunes 100times
var pages = File.ReadAllText("fortunes.db").Split("^").ToList();
const int index_page = 100;
//index page
pages[0] = @$"
  _______         __         __                __   
 |_     _|.-----.|  |.-----.|  |_.-----.--.--.|  |_ 
   |   |  |  -__||  ||  -__||   _|  -__|_   _||   _|
   |___|  |_____||__||_____||____|_____|__.__||____|

This program simulate how Teletext works
This is index(p{index_page}) page
Enter your favorate page in the range of 100 ~ 200
0: exit
We are Peyman & Hadi with laugh :)";


Console.WriteLine("Booting up...");
int index = 0;

new Thread(() =>
{
    int buffer = 0;
    bool loaded_index_again = false;
    while (true)
    {
        Console.Title = "Current buffer page is " + (index + index_page);


        // load index every 10 page again
        if (index % 10 == 0 && index != 0)
        {
            loaded_index_again = true;
            buffer = index;
            index = 0;
            Thread.Sleep(50);
            continue;

        }

        if (loaded_index_again)
        {
            // recover last index page
            index = buffer;
            loaded_index_again = false;
        }

        index++;
        if (index > pages.Count)
            index = 0;

        Thread.Sleep(50); // simulate delay
    }
}).Start();



LoadPage();


while (true)
{
    Console.Write("Enter page number p(100~200): ");

    int page_number = 100;

    try
    {
        page_number = Int32.Parse(Console.ReadLine());

        // 0: for exit
        if (page_number == 0)
        {
            Console.WriteLine("See you later!");
            Console.ReadKey();
            Environment.Exit(0);
        }

        // validate page number 
        if (page_number < 100 || page_number > 200)
        {
            Console.WriteLine("Page number should be p(100~200)!");
            continue;
        }
    }
    catch
    {
        Console.WriteLine("Page number should be a number!");
        continue;
    }

    LoadPage(page_number);
}



void LoadPage(int page_number = index_page)
{
    Console.WriteLine("Looking for p" + page_number);
    Console.Write("\rLoading p" + (index + index_page + 1) + "/200");
    while (page_number - index_page != index)
    {
        Console.Write("\rLoading p" + (index + index_page + 1) + "/200");

    }
    Console.WriteLine("\rLoading p" + (page_number) + "/200");
    page_number -= 100;
    var content = pages[page_number];
    Print(content, page_number);
}

void Print(string text, int page_number)
{

    var line = "────────────────────────────────────────────────────────────────────────────────";
    Console.WriteLine("Page: " + page_number);
    Console.WriteLine(line);
    Console.WriteLine(text);
    Console.WriteLine(line);
}
