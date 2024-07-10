import React from "react";

interface CodeIconProps {
  // İhtiyaç duyulan props'lar burada tanımlanabilir
  // Örneğin, className gibi
  className?: string;
}

export const CodeIcon: React.FC<CodeIconProps> = (props) => (
    <svg
    xmlns="http://www.w3.org/2000/svg"
    width="100%" // İstediğiniz genişlik oranı veya sabit piksel değeri
    height="100%" // İstediğiniz yükseklik oranı veya sabit piksel değeri
    viewBox="0 0 512 512"
    {...props}
  >
    <style>{`.st0{fill:#ebb447;}`}</style>
    <g>
      <path
        className="st0"
        d="M481.8,224.4c-0.4-1.8-1.5-3.4-3.1-4.4l-84.5-54.2l17.7-27.5c1-1.6,1.3-3.5,0.9-5.3c-0.4-1.8-1.5-3.4-3.1-4.4   l-150-96.2c-2.3-1.5-5.3-1.5-7.6,0l-150,96.2c-1.6,1-2.7,2.6-3.1,4.4c-0.4,1.8-0.1,3.7,0.9,5.3l17.7,27.5L33.2,220   c-1.6,1-2.7,2.6-3.1,4.4c-0.4,1.8-0.1,3.7,0.9,5.3l33.2,51.7c2.1,3.3,6.4,4.2,9.7,2.1l182-116.7l182,116.7c1.2,0.8,2.5,1.1,3.8,1.1   c2.3,0,4.6-1.1,5.9-3.2l33.2-51.7C481.9,228.1,482.2,226.2,481.8,224.4z M256,46.6l140.4,90l-13.9,21.6L259.8,79.5   c-1.2-0.7-2.5-1.1-3.8-1.1s-2.6,0.4-3.8,1.1l-122.7,78.7l-13.9-21.6L256,46.6z M439.7,267.9L259.8,152.6c-2.3-1.5-5.3-1.5-7.6,0   L72.3,267.9l-25.6-40l84.4-54.1c0,0,0.1,0,0.1-0.1L256,93.7l124.8,80.1l84.5,54.2L439.7,267.9z"
      />
      <path
        className="st0"
        d="M162.7,265.8h-11.4c-1.3,0-2.7,0.4-3.8,1.1l-34.6,22.2c-2,1.3-3.2,3.5-3.2,5.9v95.9H93c-3.9,0-7,3.1-7,7v30.9   H60c-3.9,0-7,3.1-7,7v37.9c0,3.9,3.1,7,7,7h415c3.9,0,7-3.1,7-7v-37.9c0-3.9-3.1-7-7-7h-26v-30.9c0-3.9-3.1-7-7-7h-27.4v-88.1   c0-2.4-1.2-4.6-3.2-5.9l-46-29.5c-2.2-1.4-4.9-1.5-7.1-0.2c-2.2,1.2-3.6,3.6-3.6,6.1v117.6H286V220.4c0-2.4-1.2-4.6-3.2-5.9   l-23-14.8c-2.3-1.5-5.3-1.5-7.6,0l-23,14.8c-2,1.3-3.2,3.5-3.2,5.9v170.5h-56.3V272.8C169.7,268.9,166.5,265.8,162.7,265.8z    M123.7,298.8l29.7-19h2.3v111.1h-32V298.8z M468,466.8H67v-23.9h26h349h26V466.8z M435,428.8H100v-23.9h16.7h46H233h46h82.6h46   H435V428.8z M368.6,286.1l32,20.5v84.2h-32V286.1z M240,224.2l16-10.3l16,10.3v166.7h-32V224.2z"
      />
    </g>
  </svg>
);

interface HamburgerMenuProps {
  // İhtiyaç duyulan props'lar burada tanımlanabilir
  // Örneğin, className gibi
  className?: string;
}

export const HamburgerMenuOpen: React.FC<HamburgerMenuProps> = (props) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    width="100%"
    height="100%"
    viewBox="0 0 24 24"
    {...props}
  >
    <path fill="rgba(255, 255, 255, 0)" d="M0 0h24v24H0z" />
    <path
      fill="none"
      stroke="currentColor"
      strokeLinecap="round"
      strokeLinejoin="round"
      strokeWidth={2}
      d="M3 17h18M3 12h18M3 7h18"
    />
  </svg>
);

export const HamburgerMenuClose: React.FC<HamburgerMenuProps> = (props) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    width="100%"
    height="100%"
    viewBox="0 0 24 24"
    {...props}
  >
    <path fill="rgba(255, 255, 255, 0)" d="M0 0h24v24H0z" />
    <g fill="none" fillRule="evenodd">
      <path d="M24 0v24H0V0h24ZM12.593 23.258l-.011.002-.071.035-.02.004-.014-.004-.071-.035c-.01-.004-.019-.001-.024.005l-.004.01-.017.428.005.02.01.013.104.074.015.004.012-.004.104-.074.012-.016.004-.017-.017-.427c-.002-.01-.009-.017-.017-.018Zm.265-.113-.013.002-.185.093-.01.01-.003.011.018.43.005.012.008.007.201.093c.012.004.023 0 .029-.008l.004-.014-.034-.614c-.003-.012-.01-.02-.02-.022Zm-.715.002a.023.023 0 0 0-.027.006l-.006.014-.034.614c0 .012.007.02.017.024l.015-.002.201-.093.01-.008.004-.011.017-.43-.003-.012-.01-.01-.184-.092Z" />
      <path
        fill="currentColor"
        d="m12 14.121 5.303 5.304a1.5 1.5 0 0 0 2.122-2.122L14.12 12l5.304-5.303a1.5 1.5 0 1 0-2.122-2.121L12 9.879 6.697 4.576a1.5 1.5 0 1 0-2.122 2.12L9.88 12l-5.304 5.303a1.5 1.5 0 1 0 2.122 2.122L12 14.12Z"
      />
    </g>
  </svg>
);
