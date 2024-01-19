import React from 'react'
import { Card, CardHeader, CardMedia, CardActions, IconButton, Stack, Tooltip } from '@mui/material'
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import ThumbUpAltOutlinedIcon from '@mui/icons-material/ThumbUpAltOutlined'
import ThumbDownIcon from '@mui/icons-material/ThumbDown'
import ThumbDownAltOutlinedIcon from '@mui/icons-material/ThumbDownAltOutlined';
import { formatDate } from '../../commons/utils';
import { reactionTypeEnum } from '../../commons/enums'
class Movie extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            liked: this.props.liked,
            likeCount: this.props.likeCount,
            disliked: this.props.disliked,
            dislikeCount: this.props.dislikeCount,
        };
    }
    async OnReaction(reactionType) {
        if (reactionType === reactionTypeEnum.Like && (this.state.disliked || !this.state.liked)) {
            var isSuccess = this.props.handleReact(this.props, reactionType);
            if (isSuccess) {
                this.setState({
                    liked: true,
                    likeCount: this.state.likeCount + 1,
                    disliked: false,
                    dislikeCount: this.state.dislikeCount > 0 ? this.state.dislikeCount - 1 : 0
                })
            }
        } else if (reactionType === reactionTypeEnum.Dislike && (!this.state.disliked || this.state.liked)) {
            var isSuccess = this.props.handleReact(this.props, reactionType);
            if (isSuccess) {
                this.setState({
                    liked: false,
                    likeCount: this.state.likeCount > 0 ? this.state.likeCount - 1 : 0,
                    disliked: true,
                    dislikeCount: this.state.dislikeCount + 1,
                })
            }
        }
    }
    render() {
        return (
            <Card variant="outlined" style={{ padding: '0px 30px' }}>
                <CardHeader
                    titleTypographyProps={{ fontWeight: 'bold' }}
                    title={this.props.title}
                    subheader={formatDate(this.props.createDate)}
                />
                <CardMedia
                    component="img"
                    image={this.props.thumbnail}
                    alt={this.props.title}
                />
                <CardActions >
                    <Stack width={'100%'} alignItems={'center'} direction={'row'} justifyContent={'space-between'}>
                        <Stack alignItems={'center'} direction={'row'} gap={2}>
                            <Tooltip title={this.state.liked ? 'Liked' : 'Like movie'}>
                                <IconButton onClick={() => this.OnReaction(reactionTypeEnum.Like)} aria-label="like">
                                    {
                                        this.state.liked ?
                                            <ThumbUpIcon></ThumbUpIcon> :
                                            <ThumbUpAltOutlinedIcon></ThumbUpAltOutlinedIcon>
                                    }
                                </IconButton>
                            </Tooltip>
                            <b>{this.state.likeCount}</b>
                        </Stack>
                        <Stack alignItems={'center'} direction={'row'} gap={2}>
                            <Tooltip title={this.state.disliked ? 'Disliked' : 'Dislike movie'}>
                                <IconButton onClick={() => this.OnReaction(reactionTypeEnum.Dislike)} aria-label="dislike">
                                    {
                                        this.state.disliked ?
                                            <ThumbDownIcon></ThumbDownIcon> :
                                            <ThumbDownAltOutlinedIcon></ThumbDownAltOutlinedIcon>
                                    }
                                </IconButton>
                            </Tooltip>
                            <b>{this.state.dislikeCount}</b>
                        </Stack>
                    </Stack>
                </CardActions>
            </Card >
        )
    }
}

export default Movie