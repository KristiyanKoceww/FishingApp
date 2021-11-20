//import { useState } from 'react';
// import { useGoogleMaps } from "react-hook-google-maps";

// const Map = (props) => {
//     const { ref, map, google } = useGoogleMaps(
//       // Use your own API key, you can get one from Google (https://console.cloud.google.com/google/maps-apis/overview)
      
//       // NOTE: even if you change options later
//       {
//         center: { lat: 42.698334, lng: 23.319941 },
//         // center: { lat: props.latitude, lng: props.longitude },
//         zoom: 3,
//       },
//     );
//     console.log(map); // instance of created Map object (https://developers.google.com/maps/documentation/javascript/reference/map)
//     console.log(google); // google API object (easily get google.maps.LatLng or google.maps.Marker or any other Google Maps class)
//     return <div ref={ref} style={{ width: 1000, height: 500 }} />;
//   };

// export default Map;