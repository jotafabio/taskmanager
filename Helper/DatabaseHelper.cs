using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Org.Apache.Http.Conn;

namespace TaskManagerRefactored.Helper
{
    public class DatabaseHelper : SQLiteOpenHelper
    {
        private static String database_name = "PWRTaskMan";
        private static int database_version = 1;
        public static String db_table = "Task";
        public static String db_column = "TaskName";
        public DatabaseHelper(Context context) : base(context, database_name, null, database_version) { }
        public override void OnCreate(SQLiteDatabase db)
        {
            string query = $"CREATE TABLE {db_table} (id integer primary key autoincrement, {db_column} TEXT NOT NULL)";
            db.ExecSQL(query);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            string query = $"DROP TABLE IF EXISTS {db_table}";
            db.ExecSQL(query);
            OnCreate(db);
        }
        public void AddTask(String task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues values = new ContentValues();
            values.Put(db_column, task);
            db.InsertWithOnConflict(db_table, null, values, Android.Database.Sqlite.Conflict.Replace);
            db.Close();
        }

        public void RemoveTask(String task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            db.Delete(db_table, db_column + " = ?",new string[] { task });
            db.Close();
        }

        public List<string> getTaskList()
        {
            List<string> taskList = new List<string> ();
            SQLiteDatabase db = this.ReadableDatabase;
            ICursor cursor = db.Query(db_table, new string[] { db_column }, null, null, null, null, null);
            while (cursor.MoveToNext())
            {
                int index = cursor.GetColumnIndex(db_column);
                taskList.Add(cursor.GetString(index));
            }
            db.Close();
            return taskList;
        }
    }
}