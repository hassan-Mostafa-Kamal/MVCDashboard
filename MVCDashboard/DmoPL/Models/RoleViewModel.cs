using System.Collections.Generic;
using System;

namespace DmoPL.Models
{
	public class RoleViewModel
	{
		public string Id { get; set; }
		public string RoleName { get; set; }
		
		public RoleViewModel()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
