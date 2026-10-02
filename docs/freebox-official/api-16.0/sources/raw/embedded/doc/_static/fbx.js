var last_hash = '#plop';
var last_clone = false;

function find_parent_section(hash) {
  var e = document.getElementById(hash);
  while (e) {
    if (e.className == 'section')
      return e;
    e = e.parentNode;
  }
  return null;
}

var watch_hash = function() {
  if (last_hash == document.location.hash) {
    return;
  }

  last_hash = document.location.hash;
  var sec = document.location.hash.substr(1)
  var doc = document.querySelectorAll('.document');
  if (!doc.length)
    return;
  doc = doc[0];

  // find parent section
  var parent = find_parent_section(sec);
  if (!parent) {
    // WTF
    document.location.hash = '';
    doc.style.display = 'block';
    return;
  }

  doc.style.display = 'none';
  if (last_clone) {
    document.body.removeChild(last_clone);
  }
  last_clone = parent.cloneNode(true);
  document.body.appendChild(last_clone);
  document.body.scrollTop = 0;
};

setInterval("watch_hash()", 300);
