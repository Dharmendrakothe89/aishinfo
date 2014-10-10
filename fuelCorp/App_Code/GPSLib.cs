using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for GPSLib
/// </summary>
public class GPSLib
{
	public GPSLib()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static String PlotGPSPoints(DataTable tblPoints)
    {
        try
        {
            String Locations = "";
            String sJScript = "";
            int i = 0;
            foreach (DataRow r in tblPoints.Rows)
            {
                // bypass empty rows 
                if (r["latitude"].ToString().Trim().Length == 0)
                    continue;

                string Latitude = r["latitude"].ToString();
                string Longitude = r["longitude"].ToString();

                // create a line of JavaScript for marker on map for this record 
                Locations += Environment.NewLine + @"
                path.push(new google.maps.LatLng(" + Latitude + ", " + Longitude + @"));

                var marker" + i.ToString() + @" = new google.maps.Marker({
                    position: new google.maps.LatLng
			(" + Latitude + ", " + Longitude + @"),
                    title: '#' + path.getLength(),
                    map: map
                });";
                i++;
            }

            // construct the final script
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}