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
    path: '/',
    icon: <IoIcons.IoMdHelpCircle />,

    iconClosed: <RiIcons.RiArrowDownSFill />,
    iconOpened: <RiIcons.RiArrowUpSFill />,

    subNav: [
      {
        title: 'Create post',
        path: '/CreatePost',
        icon: <IoIcons.IoIosPaper />
      },
    ]
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
  {
    title: 'Account',
    path: '/',
    icon: <IoIcons.IoMdHelpCircle />,

    iconClosed: <RiIcons.RiArrowDownSFill />,
    iconOpened: <RiIcons.RiArrowUpSFill />,

    subNav: [
      {
        title: 'Login',
        path: '/Login',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'Register',
        path: '/Register',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'Logout',
        path: '/Logout',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'Delete user',
        path: '/DeleteUser',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'Get user by Id',
        path: '/GetUserById',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'UserDetails',
        path: '/UserDetails',
        icon: <IoIcons.IoIosPaper />
      },
    ]
  },
  {
    title: 'Fish',
    path: '/',
    icon: <IoIcons.IoMdHelpCircle />,

    iconClosed: <RiIcons.RiArrowDownSFill />,
    iconOpened: <RiIcons.RiArrowUpSFill />,

    subNav: [
      {
        title: 'All Fish',
        path: '/AllFish',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'FishPublications',
        path: '/FishPublications',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'FishWithPictures',
        path: '/FishWithPictures',
        icon: <IoIcons.IoIosPaper />
      },
    ]
  },
];