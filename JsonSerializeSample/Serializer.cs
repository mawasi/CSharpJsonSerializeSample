using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;


/*

	Utf-8のBOM付きが考慮できてないので修正が必要

*/

namespace JsonSerializeSample
{
	class Serializer
	{

		/// <summary>
		/// シリアライズ(JsonSerializerバージョン)
		/// シリアライズ対象のメンバが全部プロパティで構成されているならこちらを使うのが楽。
		/// そのままシリアライズしてくれる。汎用的なシリアライズ処理が作りやすい。
		/// </summary>
		static public void Serialize(BookShelf data, string path)
		{
			var option = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),  // 保存時にasciiコード以外はエスケープシーケンスされないようにする設定
				WriteIndented = true
			};
			string text = JsonSerializer.Serialize(data, option);

			File.WriteAllText(path, text);
		}

		/// <summary>
		/// デシリアライズ
		/// </summary>
		static public BookShelf Deserialize(string path)
		{

			string text = string.Empty;
			try
			{
				text = File.ReadAllText(path);
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}

			BookShelf data = JsonSerializer.Deserialize<BookShelf>(text);

			return data;
		}


		/// <summary>
		/// シリアライズ(Utf8JsonWriterバージョン)
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="path"></param>
		static public void Serialize2(BookShelf data, string path)
		{

			JsonWriterOptions options = new JsonWriterOptions{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),  // 保存時にasciiコード以外はエスケープシーケンスされないようにする設定
				Indented = true
			};
			using var stream = new MemoryStream();
			using var writer = new Utf8JsonWriter(stream, options);
			writer.WriteStartObject();
			writer.WriteStartArray("BookList");
			foreach(var book in data.BookList){
				writer.WriteStartObject();
				writer.WriteString("Title", book.Title);
				writer.WriteString("Author", book.Author);
				writer.WriteString("ISBN", book.ISBN);
				writer.WriteNumber("PageCount", book.PageCount);
				writer.WriteString("Description", book.Description);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.Flush();

			string json = Encoding.UTF8.GetString(stream.ToArray());
			File.WriteAllText(path, json);
		}


		/// <summary>
		/// デシリアライズ(Utf8JsonReaderバージョン)
		/// BookShelf前提のやっつけ処理
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		static public BookShelf Deserialize2(string path)
		{
	//		var options = new JsonReaderOptions{
				
	//		};

			var shelf = new BookShelf();
			BookShelf.BookData book = null;
			List<string> values = new List<string>();
			bool isInArray = false;
			ReadOnlySpan<byte> jsonReadOnlySpan = File.ReadAllBytes(path);
			var reader = new Utf8JsonReader(jsonReadOnlySpan);
			while(reader.Read()){
				JsonTokenType tokenType = reader.TokenType;
				switch(tokenType){
				case JsonTokenType.StartObject:
					if(isInArray){
						book = new BookShelf.BookData();
						values.Clear();
					}
					break;
				case JsonTokenType.EndObject:
					if(isInArray){
						book.Title = values[0];
						book.Author = values[1];
						book.ISBN = values[2];
						book.Description = values[3];
						shelf.BookList.Add(book);
					}
					break;
				case JsonTokenType.StartArray:
					isInArray = true;
					break;
				case JsonTokenType.String:
					values.Add(reader.GetString());
					break;
				case JsonTokenType.Number:
					book.PageCount = reader.GetInt32();
					break;
				case JsonTokenType.EndArray:
					isInArray = false;
					break;
				}
			}

			return shelf;
		}
	}
}
