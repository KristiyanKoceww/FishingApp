import { useGoogleMaps } from "react-hook-google-maps";

const Map = (props) => {
    const { ref, map, google } = useGoogleMaps("AIzaSyC5Uk3eSfuAuhELrfyYBf-wwa2esH00n6A",
      {
        center: { lat: props.props.Latitude, lng: props.props.Longitude },
        zoom: 12,
      },
    );
    return <div ref={ref} style={{ width: 1000, height: 500 }} />;
  };

export default Map;