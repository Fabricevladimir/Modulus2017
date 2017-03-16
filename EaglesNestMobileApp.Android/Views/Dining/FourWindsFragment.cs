/*****************************************************************************/
/*                             fourWindsFragment                             */
/*                                                                           */
/*****************************************************************************/
using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Widget;
using Android.Support.V7.Widget;
using EaglesNestMobileApp.Android.Adapters;
using EaglesNestMobileApp.Core.Model.Food;
using System;
using System.Collections.Generic;
using Android.Animation;
using Android.Views.Animations;

namespace EaglesNestMobileApp.Android.Views.Dining
{
    public class fourWindsFragment : Fragment
    {
        public MenuItem[] line1Item;
        public RecyclerView menuItemRecyclerView;
        public RecyclerView.LayoutManager menuItemLayoutManager;
        public MenuItemAdapter menuItemAdapter;
        public View menuItemLayoutView;
        public View baseRecyclerLayoutView;
        public List<View> lineList = new List<View>();
        public List<RecyclerView> menuItemRecyclerViewList = new List<RecyclerView>();
        public const int TOTAL_LINES = 7;
        public RecyclerView currentMenuItemRecyclerView;
        public RecyclerView prevMenuItemRecyclerView;
        public List<FoodLinearLayoutManager> menuItemLayoutManagerList = new List<FoodLinearLayoutManager>();


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            line1Item = MenuItems.builtInItems;

           // IListAdapter listAdapter = new ArrayAdapter<string>(Activity, Resource.Layout.FoodMenuList, line1Items);
        }

        public override View OnCreateView(LayoutInflater inflater, 
            ViewGroup container, Bundle savedInstanceState)
        {
            menuItemLayoutView = inflater.Inflate(Resource.Layout.FourWindsFragment,
                container, false);

            baseRecyclerLayoutView = inflater.Inflate(Resource.Layout.BaseRecyclerView,
                container, false);

            /* Add each Recycler View to the recycler view list */
            menuItemRecyclerViewList.Add(menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line1RecyclerView));
            menuItemRecyclerViewList.Add(menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line2RecyclerView));
            menuItemRecyclerViewList.Add(menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line3RecyclerView));
            menuItemRecyclerViewList.Add(menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line4RecyclerView));
            menuItemRecyclerViewList.Add(menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line5RecyclerView));
            menuItemRecyclerViewList.Add(menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line6RecyclerView));
            menuItemRecyclerViewList.Add(menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line7RecyclerView));

           
            /* Recycler View */
            menuItemRecyclerView =
                currentMenuItemRecyclerView =
                prevMenuItemRecyclerView =
                menuItemLayoutView.FindViewById<RecyclerView>(Resource.Id.Line1RecyclerView);
                //baseRecyclerLayoutView.FindViewById<RecyclerView>(Resource.Id.BaseRecyclerView);

            /* Layout Manager */
            for (int count = 1; count <= TOTAL_LINES; count++)
                menuItemLayoutManagerList.Add(new FoodLinearLayoutManager(Activity));
            //menuItemLayoutManager = new FoodLinearLayoutManager(Activity);

            /* Adapter */
            menuItemAdapter = new MenuItemAdapter(line1Item);

            /* Set Adapter and Layout Manager for each Recycler View         */
            menuItemRecyclerView.SetLayoutManager(menuItemLayoutManager);
            menuItemRecyclerView.SetAdapter(menuItemAdapter);
            
            for(int count = 0; count < menuItemRecyclerViewList.Count; count++)
            {            
                menuItemRecyclerViewList[count].SetLayoutManager(menuItemLayoutManagerList[count]);
                menuItemRecyclerViewList[count].SetAdapter(menuItemAdapter);
            }


            /* Add each line view to the line list                           */
            lineList.Add(menuItemLayoutView.FindViewById<View>(Resource.Id.line1));
            lineList.Add(menuItemLayoutView.FindViewById<View>(Resource.Id.line2));
            lineList.Add(menuItemLayoutView.FindViewById<View>(Resource.Id.line3));
            lineList.Add(menuItemLayoutView.FindViewById<View>(Resource.Id.line4));
            lineList.Add(menuItemLayoutView.FindViewById<View>(Resource.Id.line5));
            lineList.Add(menuItemLayoutView.FindViewById<View>(Resource.Id.line6));
            lineList.Add(menuItemLayoutView.FindViewById<View>(Resource.Id.line7));

            /* Give each line view in the list a click event                 */
            foreach (View line in lineList)
                line.Click += myClickEvent;

            /* ANIMATION                                                     */
            /*Animator hideAnimation = ObjectAnimator.OfPropertyValuesHolder(null, PropertyValuesHolder.OfFloat("scaleX", 1, 0), PropertyValuesHolder.OfFloat("scaleY", 1, 0));
            hideAnimation.SetDuration(1);
            hideAnimation.SetInterpolator(new OvershootInterpolator());

            Animator showAnimation = ObjectAnimator.OfPropertyValuesHolder(null, PropertyValuesHolder.OfFloat("scaleX", 0, 1), PropertyValuesHolder.OfFloat("scaleY", 0, 1));
            showAnimation.SetDuration(1);
            showAnimation.StartDelay = 0;
            showAnimation.SetInterpolator(new OvershootInterpolator());

            LayoutTransition viewItemTransition = new LayoutTransition();
            viewItemTransition.SetAnimator(LayoutTransitionType.Appearing, showAnimation);
            viewItemTransition.SetAnimator(LayoutTransitionType.Disappearing, hideAnimation);

            ViewGroup animatedViewGroup = menuItemLayoutView.FindViewById<ViewGroup>(Resource.Id.FourWindsAnimateViewGroup);
            animatedViewGroup.LayoutTransition = viewItemTransition;*/

            /* Use this to return your custom view for this Fragment         */
            return menuItemLayoutView;
        }

        private void myClickEvent(object sender, EventArgs e)
        {
            int lineCount = 1;

            prevMenuItemRecyclerView.Visibility = ViewStates.Gone;
            foreach (View line in lineList)
            {
                System.Diagnostics.Debug.Write("LINE: " + lineCount);
                if (sender.Equals(line))
                {
                    System.Diagnostics.Debug.Write("FOUND YOU");
                    currentMenuItemRecyclerView = menuItemRecyclerViewList[lineCount - 1];
                    currentMenuItemRecyclerView.Visibility = ViewStates.Visible;
                }
                else
                    System.Diagnostics.Debug.Write("NOT YOU");

                lineCount += 1;
            }
            prevMenuItemRecyclerView = currentMenuItemRecyclerView;



            //if (menuItemRecyclerView.Visibility == ViewStates.Visible)
            //    menuItemRecyclerView.Visibility = ViewStates.Gone;
            //else
            //    menuItemRecyclerView.Visibility = ViewStates.Visible;

            
        }

        /*public void myClickEvent(object sender, EventArgs e)
        {

            if (hideView.Visibility == ViewStates.Visible)
                hideView.Visibility = ViewStates.Gone;
            else
                hideView.Visibility = ViewStates.Visible;
        }*/

        //protected void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    var t = line1Items[position];
        //   Toast.MakeText(Activity, t, ToastLength.Short).Show();
        //}

        public void OnClick ()
        {
            System.Diagnostics.Debug.Write("help");
        }
    }
}