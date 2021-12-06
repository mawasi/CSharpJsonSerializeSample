using System;
using System.Reflection;
using System.IO;

namespace JsonSerializeSample
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var shelf = new BookShelf();
			var book = new BookShelf.BookData();
			book.Title = "問題解決のための「アルゴリズム×数学」が基礎からしっかり身につく本";
			book.Author = "米田優峻";
			book.ISBN = "978-4-297-12521-9";
			book.PageCount = 288;
			book.Description = "アルゴリズムは，プログラミングを用いて問題を解決していくには欠かせない大切な道具です。一方，アルゴリズムを理解し，そして応用できるようになるためには，ある程度の数学的知識と数学的考察力も大切です。\n本書では，中学レベル～大学教養レベルの数学的知識のうちアルゴリズム学習に必要なものについて扱うとともに，有名なアルゴリズムと典型的な数学的考察について丁寧に解説します。さらに，知識をしっかり身に付けるための例題・演習問題が全200問掲載されています。";
			shelf.BookList.Add(book);

			book = new BookShelf.BookData();
			book.Title = "Foundations of Game Engine Development, Volume 1: Mathematics 1st Edition ";
			book.Author = "Eric Lengyel";
			book.ISBN = "978-0985811747";
			book.PageCount = 200;
			book.Description = "The first volume, known as FGED1, provides a detailed introduction to the mathematics used by modern game engine programmers. The book covers the topics of linear algebra (vectors and matrices), transforms, and geometry in a conventional manner. This is followed by an introduction to Grassmann algebra and geometric algebra, where a deeper understanding can be found along with explanations for why some pieces of the conventional mathematics aren’t quite right.";
			shelf.BookList.Add(book);

			string path = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName + @"\BookShelf.json";
//			Serializer.Serialize(shelf, path);

			path = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName + @"\BookShelf2.json";
//			Serializer.Serialize2(shelf, path);
			var readshelf = Serializer.Deserialize2(path);

		}
	}
}
