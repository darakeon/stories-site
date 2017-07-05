﻿using System;
using Structure.Entities;

namespace Presentation.Helpers
{
    public class EpisodeEditionHelper
    {
        public static void CutCharacter(Scene episode)
        {
            foreach (var talk in episode.TalkList)
            {
                foreach (var piece in talk.Pieces)
                {
                    var pieceHasCharacter = !String.IsNullOrEmpty(piece.Text)
                                            && piece.Text.Contains("(");

                    if (pieceHasCharacter)
                        piece.Text = piece.Text
                            .Substring(
                                0, piece.Text.IndexOf("(")
                            )
                            .Trim();
                }
            }
        }

        public static void PutCharacter(Scene episode)
        {
            foreach (var talk in episode.TalkList)
            {
                foreach (var piece in talk.Pieces)
                {
                    piece.Text += String.Format(" ({0})", talk.Character);
                }
            }
        }
    }
}

