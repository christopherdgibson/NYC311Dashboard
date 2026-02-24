using System.Text.Json.Serialization;

namespace NYC311Dashboard.Models
{
    public class RequestModel
    {
        [JsonPropertyName("unique_key")]
        public string UniqueKey { get; set; }

        [JsonPropertyName("created_date")]
        public DateTime? CreatedDate { get; set; }

        [JsonPropertyName("closed_date")]
        public DateTime? ClosedDate { get; set; }

        [JsonPropertyName("agency")]
        public string Agency { get; set; }

        [JsonPropertyName("agency_name")]
        public string AgencyName { get; set; }

        [JsonPropertyName("problem")]
        public string Problem { get; set; }

        [JsonPropertyName("problem_detail")]
        public string ProblemDetail { get; set; }

        [JsonPropertyName("additional_details")]
        public string AdditionalDetails { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("resolution_action_updated_date")]
        public DateTime? ResolutionActionUpdatedDate { get; set; }

        [JsonPropertyName("resolution_description")]
        public string ResolutionDescription { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("incident_zip")]
        public string IncidentZip { get; set; }

        [JsonPropertyName("incident_address")]
        public string IncidentAddress { get; set; }

        [JsonPropertyName("street_name")]
        public string StreetName { get; set; }

        [JsonPropertyName("cross_street_1")]
        public string CrossStreet1 { get; set; }

        [JsonPropertyName("cross_street_2")]
        public string CrossStreet2 { get; set; }

        [JsonPropertyName("intersection_street_1")]
        public string IntersectionStreet1 { get; set; }

        [JsonPropertyName("intersection_street_2")]
        public string IntersectionStreet2 { get; set; }

        [JsonPropertyName("address_type")]
        public string AddressType { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("landmark")]
        public string Landmark { get; set; }

        [JsonPropertyName("facility_type")]
        public string FacilityType { get; set; }

        [JsonPropertyName("community_board")]
        public string CommunityBoard { get; set; }

        [JsonPropertyName("council_district")]
        public string CouncilDistrict { get; set; }

        [JsonPropertyName("police_precinct")]
        public string PolicePrecinct { get; set; }

        [JsonPropertyName("bbl")]
        public string BBL { get; set; }

        [JsonPropertyName("borough")]
        public string Borough { get; set; }

        [JsonPropertyName("x_coordinate_state_plane")]
        public string XCoordinateStatePlane { get; set; }

        [JsonPropertyName("y_coordinate_state_plane")]
        public string YCoordinateStatePlane { get; set; }

        [JsonPropertyName("open_data_channel_type")]
        public string OpenDataChannelType { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        //[JsonPropertyName("location")]
        //public Location Location { get; set; }

        [JsonPropertyName("park_facility_name")]
        public string ParkFacilityName { get; set; }

        [JsonPropertyName("park_borough")]
        public string ParkBorough { get; set; }

        [JsonPropertyName("vehicle_type")]
        public string VehicleType { get; set; }

        [JsonPropertyName("taxi_company_borough")]
        public string TaxiCompanyBorough { get; set; }

        [JsonPropertyName("taxi_pick_up_location")]
        public string TaxiPickUpLocation { get; set; }

        [JsonPropertyName("bridge_highway_name")]
        public string BridgeHighwayName { get; set; }

        [JsonPropertyName("bridge_highway_direction")]
        public string BridgeHighwayDirection { get; set; }

        [JsonPropertyName("road_ramp")]
        public string RoadRamp { get; set; }

        [JsonPropertyName("bridge_highway_segment")]
        public string BridgeHighwaySegment { get; set; }
    }

    //public class Location
    //{
    //    [JsonPropertyName("type")]
    //    public string Type { get; set; }

    //    [JsonPropertyName("coordinates")]
    //    public List<double> Coordinates { get; set; }
    //}
}
