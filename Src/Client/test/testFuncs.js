const terminal = document.getElementById('log');

export function log(msg) {
    var spanInfo = document.createElement('span');
    spanInfo.classList.add('info');
    spanInfo.innerText = '~/TERMINAL/DEBUG$ ';
    terminal.appendChild(spanInfo);

    terminal.innerHTML += msg + '<br/>';
}
