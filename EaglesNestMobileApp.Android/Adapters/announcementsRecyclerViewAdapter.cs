using System;
using Android.Support.V7.Widget;
using Android.Views;
using EaglesNestMobileApp.Android.Cards;
using System.Collections.Generic;

namespace EaglesNestMobileApp.Android.Adapters
{
    public class announcementsRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Card> _announcements;

        public announcementsRecyclerViewAdapter(List<Card> announcements)
        {
            _announcements = announcements;
        }

        public override int ItemCount => _announcements.ToArray().Length;
       

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Card card = _announcements.ToArray()[position];
            announcementViewHolder currentHolder = holder as announcementViewHolder;
            currentHolder._title.Text = card.Title;
            currentHolder._image.SetImageResource(card.Image);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AnnouncementCardLayout, parent, false);
            return new announcementViewHolder(view);
        }
    }
}