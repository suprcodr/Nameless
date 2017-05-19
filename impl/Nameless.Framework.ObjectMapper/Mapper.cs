using System;
using System.Collections.Generic;
using AutoMapper;

namespace Nameless.Framework.ObjectMapper {

    public class Mapper : IMapper {

        #region Private Fields

        private AutoMapper.IMapper _mapper;
        private IEnumerable<Profile> _profiles;

        #endregion Private Fields

        #region Public Constructors

        public Mapper(IEnumerable<Profile> profiles) {
            Prevent.ParameterNull(profiles, nameof(profiles));

            _profiles = profiles;

            Initialize();
        }

        #endregion Public Constructors

        #region Private Methods

        private void Initialize() {
            var configuration = new MapperConfiguration(cfg => {
                foreach (var profile in _profiles) {
                    cfg.AddProfile(profile);
                }
            });

            _mapper = configuration.CreateMapper();
        }

        #endregion Private Methods

        #region IMapper Members

        public object Map(object instance, Type from, Type to) {
            return _mapper.Map(instance, from, to);
        }

        #endregion IMapper Members
    }
}