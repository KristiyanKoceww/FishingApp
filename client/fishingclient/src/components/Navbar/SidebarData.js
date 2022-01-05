import React from "react";
import * as FaIcons from "react-icons/fa";
import * as IoIcons from "react-icons/io";
import * as RiIcons from "react-icons/ri";

 export const SidebarDataForAdmin = [
    {
      title: "Home",
      path: "/",
      icon: <IoIcons.IoMdPeople />,
    },
    {
      title: "Admin panel",
      path: "/AppAdmin",
      icon: <IoIcons.IoMdPeople />,
    },
    {
      title: "Knots",
      path: "/AllKnots",
      icon: <IoIcons.IoMdPeople />,

      icon: <FaIcons.FaEnvelopeOpenText />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Create knot",
          path: "/createKnot",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "Display all knots",
          path: "/AllKnots",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Countries",
      path: "/CreateCountry",
      icon: <FaIcons.FaEnvelopeOpenText />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Create country",
          path: "/CreateCountry",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Reservoir",
      path: "/AllReservoirs",
      icon: <FaIcons.FaEnvelopeOpenText />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Create reservoir",
          path: "/CreateReservoir",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "Reservoirs",
          path: "/AllReservoirs",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Account",
      path: "/UserDetails",
      icon: <IoIcons.IoMdHelpCircle />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Login",
          path: "/Login",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "Register",
          path: "/Register",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "Logout",
          path: "/Logout",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "UserDetails",
          path: "/UserDetails",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Fish",
      path: "/FishInfo",
      icon: <IoIcons.IoMdHelpCircle />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Fish for admin",
          path: "/AllFish",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "Fish",
          path: "/FishInfo",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Weather",
      path: "/Weather",
      icon: <IoIcons.IoMdHelpCircle />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Current Weather Forecast",
          path: "/Weather",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "5-days Weather Forecast",
          path: "/FiveDaysWeatherForecast",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
  ];
 export const SidebarDataForUsers = [
    {
      title: "Home",
      path: "/",
      icon: <IoIcons.IoMdPeople />,
    },
    {
      title: "Knots",
      path: "/AllKnots",
      icon: <IoIcons.IoMdPeople />,
      icon: <FaIcons.FaEnvelopeOpenText />,
      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,
      subNav: [
        {
          title: "Display all knots",
          path: "/AllKnots",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Reservoir",
      path: "/AllReservoirs",
      icon: <FaIcons.FaEnvelopeOpenText />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,
      subNav: [
        {
          title: "Reservoirs",
          path: "/AllReservoirs",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Account",
      path: "/UserDetails",
      icon: <IoIcons.IoMdHelpCircle />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Logout",
          path: "/Logout",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "UserDetails",
          path: "/UserDetails",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Fish",
      path: "/FishInfo",
      icon: <IoIcons.IoMdHelpCircle />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Fish",
          path: "/FishInfo",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
    {
      title: "Weather",
      path: "/Weather",
      icon: <IoIcons.IoMdHelpCircle />,

      iconClosed: <RiIcons.RiArrowDownSFill />,
      iconOpened: <RiIcons.RiArrowUpSFill />,

      subNav: [
        {
          title: "Current Weather Forecast",
          path: "/Weather",
          icon: <IoIcons.IoIosPaper />,
        },
        {
          title: "5-days Weather Forecast",
          path: "/FiveDaysWeatherForecast",
          icon: <IoIcons.IoIosPaper />,
        },
      ],
    },
  ];
