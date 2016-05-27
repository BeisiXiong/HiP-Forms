﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
/*Copyright (C) 2016 History in Paderborn App - Universit�t Paderborn
 
  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at
 
       http://www.apache.org/licenses/LICENSE-2.0
 
  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.*/
namespace de.upb.hip.mobile.pcl.BusinessLayer.Models
{
	using Realms;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class ViaPointData : RealmObject, IIdentifiable
	{
		//Attributes
		[ObjectId]
		private string _id{ get; set; }
		public string Id{
			get{ return _id; }
			set{ Realm.GetInstance ().Write (() => _id = value); }
		}

		private GeoPoint _location{ get; set; }
		public GeoPoint Location{
			get{ return _location; }
			set{ Realm.GetInstance ().Write (() => _location = value); }
		}

		private string _title{ get; set; }
		public string Title{
			get{ return _title; }
			set{ Realm.GetInstance ().Write (() => _title = value); }
		}

		private string _description{ get; set; }
		public string Description{
			get{ return _description; }
			set{ Realm.GetInstance ().Write (() => _description = value); }
		}

		private string _exhibitid{ get; set; }
		public string ExhibitId{
			get{ return _exhibitid; }
			set{ Realm.GetInstance ().Write (() => _exhibitid = value); }
		}

		//Associations
		// Contructor
		public ViaPointData(){
		}
	}
}

