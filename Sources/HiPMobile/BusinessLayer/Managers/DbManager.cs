﻿// Copyright (C) 2016 History in Paderborn App - Universität Paderborn
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//       http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.DataAccessLayer;
using de.upb.hip.mobile.pcl.Helpers;
using Realms;

namespace de.upb.hip.mobile.pcl.BusinessLayer.Managers {
    /// <summary>
    /// Access to database methods.
    /// </summary>
    public static class DbManager {

        private static readonly IDataAccess DataAccess = IoCManager.Resolve<IDataAccess>();

        /// <summary>
        /// Creates an object of type T that is synced to the database.
        /// </summary>
        /// <typeparam name="T">The type of the object being created. T needs to be subtype of RealmObject and implement the IIdentifiable interface.</typeparam>
        /// <returns>The instance.</returns>
        public static T CreateBusinessObject<T> () where T : RealmObject, IIdentifiable, new ()
        {
            return DataAccess.CreateObject<T> ();
        }

        /// <summary>
        /// Delete an object of type T from the database.
        /// </summary>
        /// <typeparam name="T">The type of the object. T needs to be subtype of RealmObject and implement the IIdentifiable interface.</typeparam>
        /// <param name="entitiy">The object to be deleted.</param>
        /// <returns>True if deletion was successful. False otherwise.</returns>
        public static bool DeleteBusinessEntity<T> (T entitiy) where T : RealmObject, IIdentifiable
        {
            if (entitiy != null)
            {
                return DataAccess.DeleteItem<T>(entitiy.Id);
            }
            return true;
        }

        /// <summary>
        /// Starts a new write transaction. Make sure to close the transaction by either committing it or rolling back.
        /// </summary>
        /// <returns>The transaction object which can perform committing or rolling back.</returns>
        public static BaseTransaction StartTransaction ()
        {
            return DataAccess.StartTransaction ();
        }

        public static void UpdateDatabase ()
        {
            if (DataAccess.GetVersion () < DbDummyDataFiller.DatabaseVersion)
            {
                DataAccess.DeleteDatabase ();
                DataAccess.CreateDatabase (DbDummyDataFiller.DatabaseVersion);
                new DbDummyDataFiller ().InsertData ();
                Settings.DatabaseVersion = DbDummyDataFiller.DatabaseVersion;
            }
        }

    }
}