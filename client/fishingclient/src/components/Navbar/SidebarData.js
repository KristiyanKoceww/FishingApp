import React from 'react';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';
import * as RiIcons from 'react-icons/ri';

export const SidebarData = [
  {
    title: 'Knots',
    path: '/',
    icon: <IoIcons.IoMdPeople />,

    icon: <FaIcons.FaEnvelopeOpenText />,

    iconClosed: <RiIcons.RiArrowDownSFill />,
    iconOpened: <RiIcons.RiArrowUpSFill />,

    subNav: [
      {
        title: 'Create knot',
        path: '/createKnot',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'Display all knots',
        path: '/DisplayAllKnots',
        icon: <IoIcons.IoIosPaper />
      }
    ]
  },
  {
    title: 'Countries',
    path: '/',
    icon: <FaIcons.FaEnvelopeOpenText />,

    iconClosed: <RiIcons.RiArrowDownSFill />,
    iconOpened: <RiIcons.RiArrowUpSFill />,

    subNav: [
      {
        title: 'Create country',
        path: '/CreateCountry',
        icon: <IoIcons.IoIosPaper />
      },
    ]
  },
  {
    title: 'Posts',
    path: '/Posts',
    icon: <IoIcons.IoMdHelpCircle />
  },
  {
    title: 'Reservoir',
    path: '/',
    icon: <FaIcons.FaEnvelopeOpenText />,

    iconClosed: <RiIcons.RiArrowDownSFill />,
    iconOpened: <RiIcons.RiArrowUpSFill />,

    subNav: [
      {
        title: 'Create reservoir',
        path: '/CreateReservoir',
        icon: <IoIcons.IoIosPaper />
      },
    ]
  },
];