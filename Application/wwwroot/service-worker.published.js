// Used in production after `dotnet publish`.
// The build system generates `service-worker-assets.js` (via ServiceWorkerAssetsManifest)
// which declares `self.assetsManifest` with all app asset URLs/hashes.

const CACHE = 'discogg-cache-v1';
const ASSETS = self.assetsManifest?.assets || [];

self.addEventListener('install', event => {
    event.waitUntil(
        (async () => {
            const cache = await caches.open(CACHE);
            await cache.addAll(ASSETS.map(a => a.url));
            await self.skipWaiting();
        })()
    );
});

self.addEventListener('activate', event => {
    event.waitUntil(
        (async () => {
            const keys = await caches.keys();
            await Promise.all(
                keys.filter(k => k !== CACHE).map(k => caches.delete(k))
            );
            await self.clients.claim();
        })()
    );
});

self.addEventListener('fetch', event => {
    const { origin } = self.location;

    // Only handle same-origin requests (skip Discogs API calls)
    if (!event.request.url.startsWith(origin)) return;

    // Navigation: try network, fall back to cached SPA shell
    if (event.request.mode === 'navigate') {
        event.respondWith(
            (async () => {
                try {
                    return await fetch(event.request);
                } catch {
                    return await caches.match('/Discogg/');
                }
            })()
        );
        return;
    }

    // Static assets: cache-first
    event.respondWith(
        (async () => {
            const cached = await caches.match(event.request);
            if (cached) return cached;

            const response = await fetch(event.request);
            if (response.ok) {
                const cache = await caches.open(CACHE);
                cache.put(event.request, response.clone());
            }
            return response;
        })()
    );
});