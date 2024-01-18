import React from "react";
import { Typography } from "@mui/material"
import "./index.css"

const subtitles = [
    'Where ideas meet, minds connect, and innovation takes flight.',
    'Where creativity meets technology, and you meet success.',
    'Where your journey begins, and your dreams take flight.',
    'Where connections are made, and relationships are built.',
    'Where conversations start, and collaborations are born.',
    'Where people meet, and ideas are exchanged.',
    'Where thoughts align, and partnerships are formed.',
    'Where dialogues open, and bonds are strengthened.',
    'Where minds converge, and visions are shared.',
    'Where interactions matter, and friendships take root.',
    'Where you meet like-minded individuals, and your network expands.',
    'Where every conversation is a new opportunity, and every interaction a step forward.'
];

const getRandomSubtitle = ():string => {
    const randomIndex = Math.floor(Math.random() * subtitles.length);
    return subtitles[randomIndex]
}

interface SubtitlePros{
    align?: 'left' | 'center' | 'right';
    gutterBottom?: boolean;
}

function Subtitle({ align = 'left', gutterBottom = false }: SubtitlePros) {

    const [subtitles] = React.useState<string>(getRandomSubtitle);

  return (
    <div className={gutterBottom ? 'mb-9' : 'mb-0'}>
        <Typography variant='h4' align={align} gutterBottom sx={{ color: '#000', fontWeight: 'bold' }} className='dark:text-white'>
           Let's Talk 🎉 
        </Typography>
        <Typography variant='subtitle1' align={align} gutterBottom sx={{ color: '#666', fontSize: { xs: '1rem', sm: '1.25rem', md: '2.0rem', lg:'1rem'}  }} className='dark:text-gray-400 max-sm:text-lg'>
            {subtitles}
        </Typography>
    </div>
  )
}

export default Subtitle