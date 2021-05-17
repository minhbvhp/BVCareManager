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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="BVCareManagerx86")]
	public partial class BVCareManagerDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertClaim(Claim instance);
    partial void UpdateClaim(Claim instance);
    partial void DeleteClaim(Claim instance);
    partial void InsertPolicy(Policy instance);
    partial void UpdatePolicy(Policy instance);
    partial void DeletePolicy(Policy instance);
    partial void InsertClaimsProgress(ClaimsProgress instance);
    partial void UpdateClaimsProgress(ClaimsProgress instance);
    partial void DeleteClaimsProgress(ClaimsProgress instance);
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
		
		public System.Data.Linq.Table<Claim> Claims
		{
			get
			{
				return this.GetTable<Claim>();
			}
		}
		
		public System.Data.Linq.Table<Policy> Policies
		{
			get
			{
				return this.GetTable<Policy>();
			}
		}
		
		public System.Data.Linq.Table<ClaimsProgress> ClaimsProgresses
		{
			get
			{
				return this.GetTable<ClaimsProgress>();
			}
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
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Claims")]
	public partial class Claim : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _PolicyId;
		
		private System.DateTime _ExaminationDate;
		
		private System.DateTime _ReceivedDate;
		
		private System.Nullable<System.DateTime> _PaidDate;
		
		private System.Nullable<int> _TotalPaid;
		
		private EntitySet<ClaimsProgress> _ClaimsProgresses;
		
		private EntityRef<Policy> _Policy;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPolicyIdChanging(int value);
    partial void OnPolicyIdChanged();
    partial void OnExaminationDateChanging(System.DateTime value);
    partial void OnExaminationDateChanged();
    partial void OnReceivedDateChanging(System.DateTime value);
    partial void OnReceivedDateChanged();
    partial void OnPaidDateChanging(System.Nullable<System.DateTime> value);
    partial void OnPaidDateChanged();
    partial void OnTotalPaidChanging(System.Nullable<int> value);
    partial void OnTotalPaidChanged();
    #endregion
		
		public Claim()
		{
			this._ClaimsProgresses = new EntitySet<ClaimsProgress>(new Action<ClaimsProgress>(this.attach_ClaimsProgresses), new Action<ClaimsProgress>(this.detach_ClaimsProgresses));
			this._Policy = default(EntityRef<Policy>);
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PolicyId", DbType="Int NOT NULL")]
		public int PolicyId
		{
			get
			{
				return this._PolicyId;
			}
			set
			{
				if ((this._PolicyId != value))
				{
					if (this._Policy.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPolicyIdChanging(value);
					this.SendPropertyChanging();
					this._PolicyId = value;
					this.SendPropertyChanged("PolicyId");
					this.OnPolicyIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExaminationDate", DbType="Date NOT NULL")]
		public System.DateTime ExaminationDate
		{
			get
			{
				return this._ExaminationDate;
			}
			set
			{
				if ((this._ExaminationDate != value))
				{
					this.OnExaminationDateChanging(value);
					this.SendPropertyChanging();
					this._ExaminationDate = value;
					this.SendPropertyChanged("ExaminationDate");
					this.OnExaminationDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceivedDate", DbType="Date NOT NULL")]
		public System.DateTime ReceivedDate
		{
			get
			{
				return this._ReceivedDate;
			}
			set
			{
				if ((this._ReceivedDate != value))
				{
					this.OnReceivedDateChanging(value);
					this.SendPropertyChanging();
					this._ReceivedDate = value;
					this.SendPropertyChanged("ReceivedDate");
					this.OnReceivedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PaidDate", DbType="Date")]
		public System.Nullable<System.DateTime> PaidDate
		{
			get
			{
				return this._PaidDate;
			}
			set
			{
				if ((this._PaidDate != value))
				{
					this.OnPaidDateChanging(value);
					this.SendPropertyChanging();
					this._PaidDate = value;
					this.SendPropertyChanged("PaidDate");
					this.OnPaidDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalPaid", DbType="Int")]
		public System.Nullable<int> TotalPaid
		{
			get
			{
				return this._TotalPaid;
			}
			set
			{
				if ((this._TotalPaid != value))
				{
					this.OnTotalPaidChanging(value);
					this.SendPropertyChanging();
					this._TotalPaid = value;
					this.SendPropertyChanged("TotalPaid");
					this.OnTotalPaidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Claim_ClaimsProgress", Storage="_ClaimsProgresses", ThisKey="Id", OtherKey="ClaimId")]
		public EntitySet<ClaimsProgress> ClaimsProgresses
		{
			get
			{
				return this._ClaimsProgresses;
			}
			set
			{
				this._ClaimsProgresses.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Policy_Claim", Storage="_Policy", ThisKey="PolicyId", OtherKey="Id", IsForeignKey=true)]
		public Policy Policy
		{
			get
			{
				return this._Policy.Entity;
			}
			set
			{
				Policy previousValue = this._Policy.Entity;
				if (((previousValue != value) 
							|| (this._Policy.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Policy.Entity = null;
						previousValue.Claims.Remove(this);
					}
					this._Policy.Entity = value;
					if ((value != null))
					{
						value.Claims.Add(this);
						this._PolicyId = value.Id;
					}
					else
					{
						this._PolicyId = default(int);
					}
					this.SendPropertyChanged("Policy");
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
		
		private void attach_ClaimsProgresses(ClaimsProgress entity)
		{
			this.SendPropertyChanging();
			entity.Claim = this;
		}
		
		private void detach_ClaimsProgresses(ClaimsProgress entity)
		{
			this.SendPropertyChanging();
			entity.Claim = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Policies")]
	public partial class Policy : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _Number;
		
		private string _ContractId;
		
		private string _InsuredId;
		
		private System.DateTime _FromDate;
		
		private System.DateTime _ToDate;
		
		private EntitySet<Claim> _Claims;
		
		private EntityRef<Contract> _Contract;
		
		private EntityRef<Insured> _Insured;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNumberChanging(int value);
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
			this._Claims = new EntitySet<Claim>(new Action<Claim>(this.attach_Claims), new Action<Claim>(this.detach_Claims));
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Number", DbType="Int NOT NULL")]
		public int Number
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Policy_Claim", Storage="_Claims", ThisKey="Id", OtherKey="PolicyId")]
		public EntitySet<Claim> Claims
		{
			get
			{
				return this._Claims;
			}
			set
			{
				this._Claims.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Contract_Policy", Storage="_Contract", ThisKey="ContractId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Insured_Policy", Storage="_Insured", ThisKey="InsuredId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
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
		
		private void attach_Claims(Claim entity)
		{
			this.SendPropertyChanging();
			entity.Policy = this;
		}
		
		private void detach_Claims(Claim entity)
		{
			this.SendPropertyChanging();
			entity.Policy = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ClaimsProgress")]
	public partial class ClaimsProgress : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _ClaimId;
		
		private System.DateTime _Date;
		
		private string _Remarks;
		
		private EntityRef<Claim> _Claim;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnClaimIdChanging(int value);
    partial void OnClaimIdChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnRemarksChanging(string value);
    partial void OnRemarksChanged();
    #endregion
		
		public ClaimsProgress()
		{
			this._Claim = default(EntityRef<Claim>);
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClaimId", DbType="Int NOT NULL")]
		public int ClaimId
		{
			get
			{
				return this._ClaimId;
			}
			set
			{
				if ((this._ClaimId != value))
				{
					if (this._Claim.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnClaimIdChanging(value);
					this.SendPropertyChanging();
					this._ClaimId = value;
					this.SendPropertyChanged("ClaimId");
					this.OnClaimIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Remarks", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Remarks
		{
			get
			{
				return this._Remarks;
			}
			set
			{
				if ((this._Remarks != value))
				{
					this.OnRemarksChanging(value);
					this.SendPropertyChanging();
					this._Remarks = value;
					this.SendPropertyChanged("Remarks");
					this.OnRemarksChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Claim_ClaimsProgress", Storage="_Claim", ThisKey="ClaimId", OtherKey="Id", IsForeignKey=true)]
		public Claim Claim
		{
			get
			{
				return this._Claim.Entity;
			}
			set
			{
				Claim previousValue = this._Claim.Entity;
				if (((previousValue != value) 
							|| (this._Claim.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Claim.Entity = null;
						previousValue.ClaimsProgresses.Remove(this);
					}
					this._Claim.Entity = value;
					if ((value != null))
					{
						value.ClaimsProgresses.Add(this);
						this._ClaimId = value.Id;
					}
					else
					{
						this._ClaimId = default(int);
					}
					this.SendPropertyChanged("Claim");
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
}
#pragma warning restore 1591
