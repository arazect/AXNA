﻿using System;
using System.Collections.Generic;
using AXNAEngine.com.axna.entity;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.worlds
{
    public class World
    {
        protected readonly List<BasicEntity> AddList = new List<BasicEntity>();
        protected readonly List<BasicEntity> RemoveList = new List<BasicEntity>();
        protected readonly List<BasicEntity> Entities = new List<BasicEntity>();
        protected Color ClearColor = Color.CornflowerBlue;

        public World(String name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        #region Public Methods

        public void AddEntity(BasicEntity gameEntity)
        {
            AddList.Add(gameEntity);
        }

        public void RemoveEntity(BasicEntity gameEntity)
        {
            RemoveList.Add(gameEntity);
        }

        #endregion

        #region Overridable Methods

        public virtual void OnInitialize()
        {
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
            UpdateLists();

            foreach (var entity in Entities)
            {
                if (entity.IsActive)
                {
                    entity.Update(gameTime);
                }
            }
        }

        public virtual void OnDraw(GameTime gameTime)
        {
            AXNA.GraphicsDevice.Clear(ClearColor);
            AXNA.SpriteBatch.Begin();

            foreach (var entity in Entities)
            {
                if (entity.IsVisible)
                {
                    entity.Draw(gameTime);
                }
            }

            AXNA.SpriteBatch.End();
        }

        public virtual void OnRemove()
        {
            Entities.RemoveRange(0, Entities.Count);
        }

        #endregion

        #region Internal Methods

        private void UpdateLists()
        {
            // Добавление
            foreach (var entity in AddList)
            {
                entity.ParentWorld = this;
                Entities.Add(entity);
            }

            // Удаление
            foreach (var entity in RemoveList)
            {
                entity.ParentWorld = null;
                Entities.Remove(entity);
            }

            AddList.RemoveRange(0, AddList.Count);
            RemoveList.RemoveRange(0, RemoveList.Count);
        }

        #endregion

        public List<T> GetEntitiesOfType<T>() where T : EngineEntity
        {
            List<T> result = new List<T>();
            foreach (var gameEntity in Entities)
            {
                if (gameEntity is T)
                {
                    result.Add(gameEntity as T);
                }
            }

            return result;
        }
    }
}