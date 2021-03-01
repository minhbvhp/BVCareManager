﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BVCareManager.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="BVCareManager")]
	public partial class BVCareManagerDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertContract(Contract instance);
    partial void UpdateContract(Contract instance);
    partial void DeleteContract(Contract instance);
    partial void InsertInsured(Insured instance);
    partial void UpdateInsured(Insured instance);
    partial void DeleteInsured(Insured instance);
    #endregion
		
		public BVCareManagerDataContext() : 
				base(global::BVCareManager.Properties.Settings.Default.BVCareManagerConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BVCareManagerDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BVCareManagerDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BVCareManagerDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BVCareManagerDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Contract> Contracts
		{
			get
			{
				return this.GetTable<Contract>();
			}
		}
		
		public System.Data.Linq.Table<Insured> Insureds
		{
			get
			{
				return this.GetTable<Insured>();
			}
		}
		
		public System.Data.Linq.Table<Policy> Policies
		{
			get
			{
				return this.GetTable<Policy>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Contracts")]
	public partial class Contract : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Id;
		
		private System.DateTime _FromDate;
		
		private System.DateTime _ToDate;
		
		private int _AnnualPremiumPerInsured;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(string value);
    partial void OnIdChanged();
    partial void OnFromDateChanging(System.DateTime value);
    partial void OnFromDateChanged();
    partial void OnToDateChanging(System.DateTime value);
    partial void OnToDateChanged();
    partial void OnAnnualPremiumPerInsuredChanging(int value);
    partial void OnAnnualPremiumPerInsuredChanged();
    #endregion
		
		public Contract()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FromDate", DbType="Date NOT NULL")]
		public System.DateTime FromDate
		{
			get
			{
				return this._FromDate;
			}
			set
			{
				if ((this._FromDate != value))
				{
					this.OnFromDateChanging(value);
					this.SendPropertyChanging();
					this._FromDate = value;
					this.SendPropertyChanged("FromDate");
					this.OnFromDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ToDate", DbType="Date NOT NULL")]
		public System.DateTime ToDate
		{
			get
			{
				return this._ToDate;
			}
			set
			{
				if ((this._ToDate != value))
				{
					this.OnToDateChanging(value);
					this.SendPropertyChanging();
					this._ToDate = value;
					this.SendPropertyChanged("ToDate");
					this.OnToDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnnualPremiumPerInsured", DbType="Int NOT NULL")]
		public int AnnualPremiumPerInsured
		{
			get
			{
				return this._AnnualPremiumPerInsured;
			}
			set
			{
				if ((this._AnnualPremiumPerInsured != value))
				{
					this.OnAnnualPremiumPerInsuredChanging(value);
					this.SendPropertyChanging();
					this._AnnualPremiumPerInsured = value;
					this.SendPropertyChanged("AnnualPremiumPerInsured");
					this.OnAnnualPremiumPerInsuredChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Insureds")]
	public partial class Insured : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Id;
		
		private string _Name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(string value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Insured()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="VarChar(30) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Policies")]
	public partial class Policy
	{
		
		private short _Number;
		
		private string _ContractId;
		
		private string _InsuredId;
		
		private System.DateTime _FromDate;
		
		private System.DateTime _ToDate;
		
		private int _Premium;
		
		public Policy()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Number", DbType="SmallInt NOT NULL")]
		public short Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				if ((this._Number != value))
				{
					this._Number = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContractId", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ContractId
		{
			get
			{
				return this._ContractId;
			}
			set
			{
				if ((this._ContractId != value))
				{
					this._ContractId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_InsuredId", DbType="VarChar(30) NOT NULL", CanBeNull=false)]
		public string InsuredId
		{
			get
			{
				return this._InsuredId;
			}
			set
			{
				if ((this._InsuredId != value))
				{
					this._InsuredId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FromDate", DbType="Date NOT NULL")]
		public System.DateTime FromDate
		{
			get
			{
				return this._FromDate;
			}
			set
			{
				if ((this._FromDate != value))
				{
					this._FromDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ToDate", DbType="Date NOT NULL")]
		public System.DateTime ToDate
		{
			get
			{
				return this._ToDate;
			}
			set
			{
				if ((this._ToDate != value))
				{
					this._ToDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Premium", DbType="Int NOT NULL")]
		public int Premium
		{
			get
			{
				return this._Premium;
			}
			set
			{
				if ((this._Premium != value))
				{
					this._Premium = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
