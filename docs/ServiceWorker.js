const cacheName = "Captain Coder's Academy-Tower Defense-0.1.0";
const contentToCache = [
    "Build/0aafecfbceb730a5b07979e3338d75ef.loader.js",
    "Build/836b5d8c15324d159dc2b0de42137745.framework.js",
    "Build/9d5a071d81abd751668416306955b9bd.data",
    "Build/c33585e46db4f5566f12adee1e6d7723.wasm",
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
