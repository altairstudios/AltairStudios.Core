using System;


namespace Invoices.Models {
	public interface IContactable {
		int? Id {
			get;
			set;
		}
		string getName();
		string getPhone();
		string getEmail();
	}
}