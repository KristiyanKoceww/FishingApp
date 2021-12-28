import { useGoogleMaps } from "react-hook-google-maps";

const Map = (props) => {
  const api = process.env.REACT_APP_GOOGLEMAPS;
    const { ref, map, google } = useGoogleMaps(api,
      {
        center: { lat: props.props.Latitude, lng: props.props.Longitude },
        zoom: 12,
      },
    );
    return <div ref={ref} style={{ width: 1000, height: 500 }} />;
  };

export default Map;