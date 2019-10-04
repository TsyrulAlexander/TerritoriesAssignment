using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Exception {
	public class ExistFileException : System.Exception {
		public ExistFileException(string message) : base(message) { }
	}
}