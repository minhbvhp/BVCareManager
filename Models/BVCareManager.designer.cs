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
    partial void InsertPolicy(Policy instance);
    partial void UpdatePolicy(Policy instance);
    partial void DeletePolicy(Policy instance);
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
		
		private EntitySet<Policy> _Policies;
		
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
			this._Policies = new EntitySet<Policy>(new Action<Policy>(this.attach_Policies), new Action<Policy>(this.detach_Policies));
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Contract_Policy", Storage="_Policies", ThisKey="Id", OtherKey="ContractId")]
		public EntitySet<Policy> Policies
		{
			get
			{
				return this._Policies;
			}
			set
			{
				this._Policies.Assign(value);
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
		
		private void attach_Policies(Policy entity)
		{
			this.SendPropertyChanging();
			entity.Contract = this;
		}
		
		private void detach_Policies(Policy entity)
		{
			this.SendPropertyChanging();
			entity.Contract = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Insureds")]
	public partial class Insured : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Id;
		
		private string _Name;
		
		private EntitySet<Policy> _Policies;
		
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
			this._Policies = new EntitySet<Policy>(new Action<Policy>(this.attach_Policies), new Action<Policy>(this.detach_Policies));
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Insured_Policy", Storage="_Policies", ThisKey="Id", OtherKey="InsuredId")]
		public EntitySet<Policy> Policies
		{
			get
			{
				return this._Policies;
			}
			set
			{
				this._Policies.Assign(value);
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
		
		private void attach_Policies(Policy entity)
		{
			this.SendPropertyChanging();
			entity.Insured = this;
		}
		
		private void detach_Policies(Policy entity)
		{
			this.SendPropertyChanging();
			entity.Insured = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Policies")]
	public partial class Policy : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private short _Number;
		
		private string _ContractId;
		
		private string _InsuredId;
		
		private System.DateTime _FromDate;
		
		private System.DateTime _ToDate;
		
		private EntityRef<Contract> _Contract;
		
		private EntityRef<Insured> _Insured;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNumberChanging(short value);
    partial void OnNumberChanged();
    partial void OnContractIdChanging(string value);
    partial void OnContractIdChanged();
    partial void OnInsuredIdChanging(string value);
    partial void OnInsuredIdChanged();
    partial void OnFromDateChanging(System.DateTime value);
    partial void OnFromDateChanged();
    partial void OnToDateChanging(System.DateTime value);
    partial void OnToDateChanged();
    #endregion
		
		public Policy()
		{
			this._Contract = default(EntityRef<Contract>);
			this._Insured = default(EntityRef<Insured>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
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
					this.OnNumberChanging(value);
					this.SendPropertyChanging();
					this._Number = value;
					this.SendPropertyChanged("Number");
					this.OnNumberChanged();
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
					if (this._Contract.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnContractIdChanging(value);
					this.SendPropertyChanging();
					this._ContractId = value;
					this.SendPropertyChanged("ContractId");
					this.OnContractIdChanged();
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
					if (this._Insured.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnInsuredIdChanging(value);
					this.SendPropertyChanging();
					this._InsuredId = value;
					this.SendPropertyChanged("InsuredId");
					this.OnInsuredIdChanged();
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Contract_Policy", Storage="_Contract", ThisKey="ContractId", OtherKey="Id", IsForeignKey=true)]
		public Contract Contract
		{
			get
			{
				return this._Contract.Entity;
			}
			set
			{
				Contract previousValue = this._Contract.Entity;
				if (((previousValue != value) 
							|| (this._Contract.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Contract.Entity = null;
						previousValue.Policies.Remove(this);
					}
					this._Contract.Entity = value;
					if ((value != null))
					{
						value.Policies.Add(this);
						this._ContractId = value.Id;
					}
					else
					{
						this._ContractId = default(string);
					}
					this.SendPropertyChanged("Contract");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Insured_Policy", Storage="_Insured", ThisKey="InsuredId", OtherKey="Id", IsForeignKey=true)]
		public Insured Insured
		{
			get
			{
				return this._Insured.Entity;
			}
			set
			{
				Insured previousValue = this._Insured.Entity;
				if (((previousValue != value) 
							|| (this._Insured.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Insured.Entity = null;
						previousValue.Policies.Remove(this);
					}
					this._Insured.Entity = value;
					if ((value != null))
					{
						value.Policies.Add(this);
						this._InsuredId = value.Id;
					}
					else
					{
						this._InsuredId = default(string);
					}
					this.SendPropertyChanged("Insured");
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
}
#pragma warning restore 1591