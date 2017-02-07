// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Maps.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants
	private const string IsModifiedKey = "isModified";
	private static readonly bool IsModifiedDefault = true;

	private const string IsDoubleClickedKey = "isDoubleClicked";
	private static readonly bool isDoubleClickedDefault = true;

	private const string AireRechercheKey = "aireRecherche";
	private static readonly double aireRechercheDefault = 5;

	private const string SurfaceMinKey = "surfaceMin";
	private static readonly double SurfaceMinDefault = 0;

	private const string SurfaceMaxKey = "surfaceMax";
	private static readonly double SurfaceMaxDefault = 50;

	private const string PrixMinKey = "prixMin";
	private static readonly double PrixMinDefault = 0;

	private const string PrixMaxKey = "prixMax";
	private static readonly double PrixMaxDefault = 500;

	private const string IsMaisonKey = "isMaison";
	private static readonly bool IsMaisonDefault = false;

	private const string IsAppartementKey = "isAppartement";
	private static readonly bool IsAppartementDefault = true;

    private const string IsSaleKey = "isSale";
    private static readonly bool IsSaleDefault = false;

    private const string IsRentalKey = "isRental";
    private static readonly bool IsRentalDefault = true;
#endregion


	public static bool isDoubleClicked
	{
		get { return AppSettings.GetValueOrDefault<bool>(IsDoubleClickedKey, isDoubleClickedDefault); }
		set { AppSettings.AddOrUpdateValue<bool>(IsDoubleClickedKey, value); }
	}

	public static double aireRecherche
	{
		get { return AppSettings.GetValueOrDefault<double>(AireRechercheKey, aireRechercheDefault); }
		set { AppSettings.AddOrUpdateValue<double>(AireRechercheKey, value); }
	}

	public static double surfaceMin
	{
		get { return AppSettings.GetValueOrDefault<double>(SurfaceMinKey, SurfaceMinDefault); }
		set { AppSettings.AddOrUpdateValue<double>(SurfaceMinKey, value); }
	}

	public static double surfaceMax
	{
		get { return AppSettings.GetValueOrDefault<double>(SurfaceMaxKey, SurfaceMaxDefault); }
		set { AppSettings.AddOrUpdateValue<double>(SurfaceMaxKey, value); }
	}

	public static double prixMin
	{
		get { return AppSettings.GetValueOrDefault<double>(PrixMinKey, PrixMinDefault); }
		set { AppSettings.AddOrUpdateValue<double>(PrixMinKey, value); }
	}

	public static double prixMax
	{
		get { return AppSettings.GetValueOrDefault<double>(PrixMaxKey, PrixMaxDefault); }
		set { AppSettings.AddOrUpdateValue<double>(PrixMaxKey, value); }
	}

	public static bool isAppartement
	{
			get { return AppSettings.GetValueOrDefault<bool>(IsAppartementKey, IsAppartementDefault); }
		set { AppSettings.AddOrUpdateValue<bool>(IsAppartementKey, value); }
	}

        public static bool isMaison
        {
            get { return AppSettings.GetValueOrDefault<bool>(IsMaisonKey, IsMaisonDefault); }
            set { AppSettings.AddOrUpdateValue<bool>(IsMaisonKey, value); }
        }

        public static bool isSale
	{
			get { return AppSettings.GetValueOrDefault<bool>(IsSaleKey, IsSaleDefault); }
		set { AppSettings.AddOrUpdateValue<bool>(IsSaleKey, value); }
	}

    public static bool isRental
    {
            get { return AppSettings.GetValueOrDefault<bool>(IsRentalKey, IsRentalDefault); }
            set { AppSettings.AddOrUpdateValue<bool>(IsRentalKey, value); }
    }
	public static bool isModified
	{
			get { return AppSettings.GetValueOrDefault<bool>(IsModifiedKey, IsModifiedDefault); }
		set { AppSettings.AddOrUpdateValue<bool>(IsModifiedKey, value); }
	}
   

}
}