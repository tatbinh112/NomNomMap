import { useEffect, useMemo, useState } from 'react'
import axios from 'axios'
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import L from 'leaflet'
import 'leaflet/dist/leaflet.css'

type Place = {
    id: string
    name: string
    address: string
    lat: number
    lng: number
    avgRating: number
}

const markerIcon = new L.Icon({
    iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
    shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    shadowSize: [41, 41],
})

export default function App() {
    const [places, setPlaces] = useState<Place[]>([])
    const [center] = useState<[number, number]>([16.0544, 108.2022]) // Đà Nẵng

    useEffect(() => {
        const [lat, lng] = center

        axios
            .get<Place[]>('http://localhost:5001/api/places/near', {
                params: { lat, lng, radiusKm: 5 },
            })
            .then((res) => setPlaces(res.data))
            .catch((err) => console.error('Load places failed:', err))
    }, [center])

    const map = useMemo(
        () => (
            <MapContainer
                center={center}
                zoom={13}
                scrollWheelZoom={true}
                zoomControl={true}
                style={{ height: '100vh', width: '100%' }}
            >
                {/* Dịu mắt hơn OSM mặc định */}
                <TileLayer
                    attribution='&copy; OpenStreetMap contributors &copy; CARTO'
                    url='https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png'
                />

                {places.map((p) => (
                    <Marker key={p.id} position={[p.lat, p.lng]} icon={markerIcon}>
                        <Popup>
                            <div style={{ minWidth: 180 }}>
                                <strong>{p.name}</strong>
                                <br />
                                <span>{p.address}</span>
                                <br />
                                <span>⭐ {p.avgRating.toFixed(1)}</span>
                            </div>
                        </Popup>
                    </Marker>
                ))}
            </MapContainer>
        ),
        [center, places]
    )

    return map
}