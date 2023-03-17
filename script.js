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
cat.src = "img/MarsOnMoon.png";
cat.style.position = "fixed";
cat.style.width = "100px";
cat.style.top = "70px"; // adjust as needed
cat.style.right = "20px"; // adjust as needed
document.body.appendChild(cat);

let direction = 1;
let position = -100;

function animateCat() {
    position += direction * 1;
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

function toggleCat() {
    cat.classList.toggle("hidden");
}

function changeCatColor() {
    const colors = ["red", "green", "blue", "orange", "purple"];
    const randomColor = colors[Math.floor(Math.random() * colors.length)];
    cat.style.filter = `hue-rotate(${Math.floor(Math.random() * 360)}deg) drop-shadow(2px 2px 4px ${randomColor})`;
}

setInterval(() => {
    animateCat();
}, 10);

setInterval(() => {
    toggleCat();
}, 2000);

setInterval(() => {
    changeCatColor();
}, 3000);

setInterval(animateCat, 10);
const title = document.querySelector('.home-content h1');

title.addEventListener('mouseover', () => {
    title.style.animation = 'wiggle 0.5s ease-in-out';
});

title.addEventListener('animationend', () => {
    title.style.animation = '';
});

const menuIcon = document.querySelector('.menu-icon');
const navMenu = document.querySelector('.nav-menu');

menuIcon.addEventListener('click', () => {
    navMenu.classList.toggle('active');
});


/* social links highlighting */
const contactLink = document.querySelector('#contact-link');
const socialLinks = document.querySelectorAll('.social-links a');

contactLink.addEventListener('click', (event) => {
    event.preventDefault(); // prevent default scrolling behavior

    socialLinks.forEach((link) => {
        link.classList.add('highlight-icon', 'grow-icon');

        // remove highlight and grow classes after animation completes
        setTimeout(() => {
            link.classList.remove('highlight-icon', 'grow-icon');
        }, 1000);
    });
});