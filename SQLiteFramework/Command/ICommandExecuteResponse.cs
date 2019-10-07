using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Command {
	interface ICommandExecuteResponse {
		IEnumerable<ICommandExecuteResponseItem> Items { get; set; }
	}
}
