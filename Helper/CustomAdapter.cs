using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TaskManagerRefactored.Helper
{
    public class CustomAdapter : BaseAdapter
    {
        private MainActivity mainActivity;
        private List<string> taskList;
        private DatabaseHelper databaseHelper;

        public CustomAdapter(MainActivity mainActivity, List<string> taskList, DatabaseHelper databaseHelper)
        {
            this.mainActivity = mainActivity;
            this.taskList = taskList;
            this.databaseHelper = databaseHelper;
        }

        public override int Count {
            get
            {
                return taskList.Count;
            }
        
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainActivity.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.row_detail, null);
            TextView txtTask = view.FindViewById<TextView>(Resource.Id.task_title);
            Button btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);
            //ImageButton btnAdd = view.FindViewById<ImageButton>(Resource.Id.action_add);

            txtTask.Text = taskList[position];
            btnDelete.Click += delegate
            {
                string task = taskList[position];
                databaseHelper.RemoveTask(task);
                mainActivity.LoadTaskList();
            };
            return view;
        }
    }
}