using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        private DbConnection conn;

        public PhonebookDAO()
        {
            conn = new DbConnection();
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par nom
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        public DataTable getAll()
        {
            string _query = $"SELECT * FROM [Contacts]";
            return conn.ExecuteSelectQuery(_query, null);
        }

        public int DeleteId(int id)
        {
            string _query = $"DELETE FROM [Contacts] WHERE ContactID = @_id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = id;

            return conn.ExecutUpdateQuery(_query, parameters);
        }

        public int UpdateContact(ContactModel contact)
        {
            string _query = $"UPDATE [Contacts] SET ContactID = @_contactID, LastName = @_lastName, FirstName = @_firstName, Email = @_email, Phone = @_phone, Mobile = @_mobile WHERE ContactID = @_contactID";
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@_contactID", SqlDbType.Int);
            parameters[0].Value = contact.ContactID;

            parameters[1] = new SqlParameter("@_lastName", SqlDbType.VarChar);
            parameters[1].Value = contact.LastName;

            parameters[2] = new SqlParameter("@_firstName", SqlDbType.VarChar);
            parameters[2].Value = contact.FirstName;

            parameters[3] = new SqlParameter("@_email", SqlDbType.VarChar);
            parameters[3].Value = contact.Email;

            parameters[4] = new SqlParameter("@_phone", SqlDbType.VarChar);
            parameters[4].Value = contact.Phone;

            parameters[5] = new SqlParameter("@_mobile", SqlDbType.VarChar);
            parameters[5].Value = contact.Mobile;

            return conn.ExecutUpdateQuery(_query, parameters);
        }
    }
}
