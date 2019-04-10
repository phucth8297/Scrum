package md50383821eb9844e59745c87927fd6f1eb;


public class ChatListViewRenderer
	extends md5a5af569a83c1a3f5c3a8a63bd7d39860.ItemsControlRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.ConversationalUIRenderer.Android.ChatListViewRenderer, Telerik.XamarinForms.ConversationalUI", ChatListViewRenderer.class, __md_methods);
	}


	public ChatListViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ChatListViewRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ConversationalUIRenderer.Android.ChatListViewRenderer, Telerik.XamarinForms.ConversationalUI", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public ChatListViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ChatListViewRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ConversationalUIRenderer.Android.ChatListViewRenderer, Telerik.XamarinForms.ConversationalUI", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public ChatListViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ChatListViewRenderer.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ConversationalUIRenderer.Android.ChatListViewRenderer, Telerik.XamarinForms.ConversationalUI", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
