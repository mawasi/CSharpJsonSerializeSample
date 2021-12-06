using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Jsonシリアライズ用のサンプルデータ
/// </summary>
namespace JsonSerializeSample
{
	class BookShelf
	{
		/// <summary>
		/// 本情報
		/// </summary>
		public class BookData
		{
			/// <summary>
			/// 本のタイトル
			/// </summary>
			public string Title{
				get; set;
			} = string.Empty;

			/// <summary>
			/// 著者
			/// </summary>
			public string Author{
				get; set;
			} = string.Empty;

			/// <summary>
			/// 国際標準図書番号
			/// </summary>
			public string ISBN{
				get; set;
			} = string.Empty;

			/// <summary>
			/// ページ数
			/// </summary>
			public int PageCount{
				get; set;
			} = 0;

			/// <summary>
			/// 説明
			/// </summary>
			public string Description{
				get; set;
			} = string.Empty;
		}

		public List<BookData>	BookList{
			get; set;
		} = new List<BookData>();
	}
}
