const cacheName = "Captain Coder's Academy-Tower Defense-0.1.0";
const contentToCache = [
    "Build/d439434c2ac2be13a5cef8c1f87315e5.loader.js",
    "Build/7a8911b2b8aa458db4065baf594d2161.framework.js",
    "Build/4c7da61276108b993e86ee761f687954.data",
    "Build/ff0bcaeaf61ba05fd67d2636f56679b3.wasm",
    "TemplateData/style.css"

];

self.addEventListener('install', function (e) {
    console.log('[Service Worker] Install');
    
    e.waitUntil((async function () {
      const cache = await caches.open(cacheName);
      console.log('[Service Worker] Caching all: app shell and content');
      await cache.addAll(contentToCache);
    })());
});

self.addEventListener('fetch', function (e) {
    e.respondWith((async function () {
      let response = await caches.match(e.request);
      console.log(`[Service Worker] Fetching resource: ${e.request.url}`);
      if (response) { return response; }

      response = await fetch(e.request);
      const cache = await caches.open(cacheName);
      console.log(`[Service Worker] Caching new resource: ${e.request.url}`);
      cache.put(e.request, response.clone());
      return response;
    })());
});
