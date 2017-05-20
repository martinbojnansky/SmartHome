package md504eff35830278f383b036c1568744a86;


public class TimeSpanPickerRenderer
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.TimePickerRenderer
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("SmartMote.Droid.Controls.TimeSpanPickerRenderer, SmartMote.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TimeSpanPickerRenderer.class, __md_methods);
	}


	public TimeSpanPickerRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == TimeSpanPickerRenderer.class)
			mono.android.TypeManager.Activate ("SmartMote.Droid.Controls.TimeSpanPickerRenderer, SmartMote.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public TimeSpanPickerRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == TimeSpanPickerRenderer.class)
			mono.android.TypeManager.Activate ("SmartMote.Droid.Controls.TimeSpanPickerRenderer, SmartMote.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public TimeSpanPickerRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == TimeSpanPickerRenderer.class)
			mono.android.TypeManager.Activate ("SmartMote.Droid.Controls.TimeSpanPickerRenderer, SmartMote.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

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
