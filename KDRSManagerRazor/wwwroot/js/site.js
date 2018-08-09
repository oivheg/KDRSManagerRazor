// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// This works on all devices/browsers, and uses IndexedDBShim as a final fallback
var indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB || window.shimIndexedDB;

// Open (or create) the database
var open = indexedDB.open("ServerDb", 1);

// Create the schema
open.onupgradeneeded = function () {
    var db = open.result;
    var store = db.createObjectStore("ServerStore", { keyPath: "id" });
    //var index = store.createIndex("ServerIndex", ["name.last", "name.first"]);
};

open.onsuccess = function () {
    // Start a new transaction
    var db = open.result;
    var tx = db.transaction("ServerStore", "readwrite");
    var store = tx.objectStore("ServerStore");
    //var index = store.index("ServerIndex");

    // Add some data
    store.put({ id: 12345, ip: "91.192.221.234", user: 999, pw: "Fuglekasser" });
    store.put({ id: 12345, ip: "91.192.221.162", user: 999, pw: "Fuglekasser" });
    //store.put({ id: 67890, name: { first: "Bob", last: "Smith" }, age: 35 });

    // Query the data
    var getJohn = store.get(12345);
    //var getBob = index.get(["Smith", "Bob"]);

    getJohn.onsuccess = function () {
        console.log(getJohn.result.ip);  // => "John"
    };

    //getBob.onsuccess = function () {
    //    console.log(getBob.result.name.first);   // => "Bob"
    //};

    // Close the db when the transaction is done
    tx.oncomplete = function () {
        db.close();
    };
}