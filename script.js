const animateItems = document.querySelectorAll('.animate');

const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('animate__animated', 'animate__fadeInUp');
            observer.unobserve(entry.target);
        }
    });
}, {
    rootMargin: '0% 0% -90% 0%'
});

animateItems.forEach(item => {
    observer.observe(item);
});

const cat = document.createElement("img");
cat.src = "https://cdn.pixabay.com/photo/2014/04/13/20/49/cat-323262_960_720.jpg";
cat.style.position = "fixed";
cat.style.width = "100px";
cat.style.top = "50%";
cat.style.left = "-100px";
document.body.appendChild(cat);

let direction = 1;
let position = -100;

function animateCat() {
    position += direction * 2;
    if (position > window.innerWidth) {
        direction = -1;
        cat.style.transform = "scaleX(-1)";
    }
    if (position < -100) {
        direction = 1;
        cat.style.transform = "scaleX(1)";
    }
    cat.style.left = position + "px";
}

setInterval(animateCat, 10);
const title = document.querySelector('.home-content h1');

title.addEventListener('mouseover', () => {
    title.style.animation = 'wiggle 0.5s ease-in-out';
});

title.addEventListener('animationend', () => {
    title.style.animation = '';
});