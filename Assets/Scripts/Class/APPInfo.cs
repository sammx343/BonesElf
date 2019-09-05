using System;
using System.Collections.Generic;

[Serializable]
public class APPInfo
{
	public int page;
	public int size;
	public int domain;
	public string row_model;

	public List<User> rows;
}